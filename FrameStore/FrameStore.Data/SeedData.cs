using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using EFCore.BulkExtensions;
using FrameStore.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FrameStore.Data
{
    public static class SeedData
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetRequiredService<FrameStoreContext>())
            {
                if (db.Brands.Any())
                {
                    return;
                }
            }

            var csvData = GetData();

            //Add materials to db
            var materials = GetMaterialsFromCSVData(csvData);
            using (var db = serviceProvider.GetRequiredService<FrameStoreContext>())
            {
                db.Materials.AddRange(materials);
                db.SaveChanges();
            }

            //Add brands to db.
            var brandsToAdd = GetBrandsFromCSVData(csvData, materials);
            using (var db = serviceProvider.GetRequiredService<FrameStoreContext>())
            {
                db.Brands.AddRange(brandsToAdd);
                db.SaveChanges();
            }

            //Add tracing radii for each frame using bulk insert extension.
            using (var db = serviceProvider.GetRequiredService<FrameStoreContext>())
            {
                var frames = db.Frames
                    .Include(f => f.Style.Collection.Brand)
                    .ToList();
                var tracingRadii = GetTracingRadii(frames, csvData);
                db.BulkInsert(tracingRadii);
            }
        }

        private static List<Material> GetMaterialsFromCSVData(List<CSVFrameData> csvData)
        {
            return csvData.SelectMany(f => f.Material.Split(',')).Distinct().Select(g => new Material()
            {
                MaterialId = Guid.NewGuid(),
                Name = g
            }).ToList();
        }

        private static List<Brand> GetBrandsFromCSVData(List<CSVFrameData> csvData, List<Material> materials)
        {
            var brandsToAdd = new List<Brand>();
            csvData.GroupBy(x => x.BrandName).ToList().ForEach(t =>
            {
                brandsToAdd.Add(GetBrands(t, materials));
            });

            return brandsToAdd;
        }

        private static Brand GetBrands(
           IGrouping<string, CSVFrameData> brandGrouping,
           List<Material> materials)
        {
            var brand = new Brand()
            {
                BrandId = Guid.NewGuid(),
                Name = brandGrouping.Key
            };

            //Add collection to the brand
            brandGrouping.GroupBy(h => h.CollectionName).ToList().ForEach(y =>
            {
                brand.Collections.Add(GetCollections(
                    y,
                    brand,
                    materials));
            });

            return brand;
        }

        private static Collection GetCollections(
            IGrouping<string, CSVFrameData> brandCollectionGrouping,
            Brand brand,
            List<Material> materials)
        {
            var collection = new Collection()
            {
                CollectionId = Guid.NewGuid(),
                Name = brandCollectionGrouping.Key,
                BrandId = brand.BrandId
            };

            //Add Style to the collection
            brandCollectionGrouping.GroupBy(c => c.StyleName).ToList().ForEach(s =>
            {

                collection.Styles.Add(GetStyles(
                    s,
                    collection,
                    materials));
            });

            return collection;
        }

        private static Style GetStyles(
            IGrouping<string, CSVFrameData> styleGrouping,
            Collection collection,
            List<Material> materials)
        {
            var style = new Style()
            {
                StyleId = Guid.NewGuid(),
                Name = styleGrouping.Key,
                CollectionId = collection.CollectionId
            };

            //Add frame to the style.
            styleGrouping.ToList().ForEach(f =>
            {
                var frame = new Frame()
                {
                    FrameId = Guid.NewGuid(),
                    FrameColor = f.FrameColor,
                    Vertical = f.Vertical,
                    Horizontal = f.Horizontal,
                    Bridge = f.Bridge,
                    SKU = f.SKU,
                    StyleId = style.StyleId,
                };

                frame.Materials.AddRange(GetFrameMaterials(frame, f, materials));
                style.Frames.Add(frame);
            });

            return style;
        }

        private static List<FrameMaterial> GetFrameMaterials(
            Frame frame,
            CSVFrameData frameData,
            List<Material> materials)
        {
            var result = new List<FrameMaterial>();
            var frameMaterials = frameData.Material.Split(',');
            foreach (var s in frameMaterials.ToList())
            {
                var material = materials.FirstOrDefault(m => m.Name.Equals(s));

                result.Add(new FrameMaterial()
                {
                    FrameMaterialId = Guid.NewGuid(),
                    FrameId = frame.FrameId,
                    MaterialId = material.MaterialId
                });
            }

            return result;
        }

        private static List<FrameTracingRadii> GetTracingRadii(List<Frame> frames, List<CSVFrameData> csvData)
        {
            var result = new List<FrameTracingRadii>();
            var g = csvData.GroupBy(h => new { h.BrandName, h.CollectionName, h.StyleName, h.FrameColor });

            frames.ForEach(frame =>
            {
                var frameData = csvData.AsParallel().FirstOrDefault(t => t.BrandName.Equals(frame.Style.Collection.Brand.Name) &&
                                                      t.CollectionName.Equals(frame.Style.Collection.Name) &&
                                                      t.StyleName.Equals(frame.Style.Name) &&
                                                      t.FrameColor.Equals(frame.FrameColor) &&
                                                      t.Horizontal.Equals(frame.Horizontal) &&
                                                      t.Vertical.Equals(frame.Vertical) &&
                                                      t.Bridge.Equals(frame.Bridge));

                if (null != frameData && !string.IsNullOrEmpty(frameData.TracingRadii))
                {
                    var frameTracingRadii = frameData.TracingRadii.Split(',').Select(Convert.ToDouble).ToList();
                    for (var idx = 0; idx < frameTracingRadii.Count; idx++)
                    {
                        result.Add(new FrameTracingRadii()
                        {
                            FrameTracingRadiusId = Guid.NewGuid(),
                            FrameId = frame.FrameId,
                            Radius = frameTracingRadii[idx]
                        });
                    }
                }
            });

            return result;
        }


        /// <summary>
        /// Get the CSV data to seed from.
        /// </summary>
        /// <returns>List of frame data from CSV file.</returns>
        private static List<CSVFrameData> GetData()
        {
            var result = new List<CSVFrameData>();
            using var reader = new StreamReader("FrameData.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var data = csv.GetRecords<CSVFrameData>();
            if (null != data)
            {
                result = data.ToList();
            }

            return result;
        }
    }
}