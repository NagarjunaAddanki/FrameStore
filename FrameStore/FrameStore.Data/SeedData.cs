using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using FrameStore.Data.Migrations;
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

            //Add materials
            var materials = csvData.SelectMany(f => f.Material.Split(',')).Distinct().Select(g => new Material()
            {
                MaterialId = Guid.NewGuid(),
                Name = g
            }).ToList();

            using (var db = serviceProvider.GetRequiredService<FrameStoreContext>())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = false;

                db.Materials.AddRange(materials);
                db.SaveChanges();
            }

            var brandsToAdd = new List<Brand>();
            csvData.GroupBy(x => x.BrandName).ToList().ForEach(t =>
            {
                using (var db = serviceProvider.GetRequiredService<FrameStoreContext>())
                {
                    db.ChangeTracker.AutoDetectChangesEnabled = false;
                    db.Brands.Add(AddBrand(t, materials));
                    db.SaveChanges();
                }
            });
        }

        private static Brand AddBrand(
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
                brand.Collections.Add(GetCollection(
                    y,
                    brand,
                    materials));
            });

            return brand;
        }

        private static Collection GetCollection(
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

                collection.Styles.Add(GetStyle(
                    s,
                    collection,
                    materials));
            });

            return collection;
        }

        private static Style GetStyle(
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

                frame.TracingRadii.AddRange(GetTracingRadius(frame, f));
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

        private static List<FrameTracingRadii> GetTracingRadius(
            Frame frame,
            CSVFrameData frameData)
        {
            var result = new List<FrameTracingRadii>();
            if (!string.IsNullOrEmpty(frameData.TracingRadii))
            {
                var frameTracingRadii = frameData.TracingRadii.Split(',').Select(Convert.ToDouble).ToList();
                for (var idx = 0; idx < frameTracingRadii.Count; idx++)
                {
                    result.Add(new FrameTracingRadii()
                    {
                        FrameId = frame.FrameId,
                        Radius = frameTracingRadii[idx]
                    });
                }
            }
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
