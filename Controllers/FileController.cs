﻿using Microsoft.AspNetCore.Mvc;

namespace case2_020124.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("fileNames")]
        public IActionResult GetFileNames(string filePath)
        {
            try
            {
                List<string> fileNames = Directory.GetFileSystemEntries(filePath)
                                                  .Select(Path.GetFileName)
                                                  .ToList();

                return Ok(new { FileNames = fileNames });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = ex.Message });
            }
        }

        [HttpGet("filterFileNamesByFileExtension")]
        public IActionResult GetFilteredFileNamesByFileExtension(string filePath, string? keyword)
        {
            try
            {
                List<string> fileNames = Directory.GetFileSystemEntries(filePath)
                                                           .Select(Path.GetFileName)
                                                           .ToList();
                if (keyword.Contains("*"))
                {
                    fileNames = fileNames.Where(p => p.StartsWith(keyword.Split('*')[0]) && Path.GetExtension(p) == keyword.Split('*')[1]).ToList();
                }
                else
                {
                    fileNames = fileNames.Where(file => string.IsNullOrEmpty(keyword) || Path.GetExtension(file) == keyword).ToList();

                }
                return Ok(new { FileNames = fileNames });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorMessage = ex.Message });
            }


        }
    }
}
