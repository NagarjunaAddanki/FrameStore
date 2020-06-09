using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FrameStore.Models;
using FrameStore.Service;

namespace FrameStore.Controllers
{
    public class FrameController : Controller
    {
        private readonly IFrameDataService _frameDataService;
        private readonly IMapper _mapper;
        private readonly ILogger<FrameController> _logger;

        public FrameController(IFrameDataService frameDataService, IMapper mapper, ILogger<FrameController> logger)
        {
            _frameDataService = frameDataService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _frameDataService.GetFramesAsync();
            var frameDtos = _mapper.Map<List<FrameViewModel>>(data);
            return View(frameDtos);
        }

        /// <summary>
        /// Gets the details of the given frame from the store.
        /// </summary>
        /// <param name="id">Id of the frame being requested.</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frame = await _frameDataService.GetFrameAsync(id.Value);
            if (frame == null)
            {
                return NotFound();
            }

            var frameDto = _mapper.Map<FrameViewModel>(frame);
            return View(frameDto);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var frame = await _frameDataService.GetFrameAsync(id.Value);
            if (frame == null)
            {
                return NotFound();
            }

            var frameDto = _mapper.Map<FrameViewModel>(frame);
            return View(frameDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _frameDataService.DeleteFrame(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
