using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace case2_020124.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpGet("fileNames")]
        public IActionResult GetFileNames(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("filePath cannot be null!");
            }
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
        public IActionResult GetFilteredFileNamesByFileExtension(string? filePath, string? keyword)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return BadRequest("filePath cannot be null!");
            }
            try
            {
                List<string> fileNames = Directory.GetFileSystemEntries(filePath)
                                                           .Select(Path.GetFileName)
                                                           .ToList();
                if (!string.IsNullOrEmpty(keyword) && keyword.Contains("*"))
                {
                    fileNames = fileNames.Where(p => p.ToLower().StartsWith(keyword.ToLower().Split('*')[0]) && Path.GetExtension(p) == keyword.ToLower().Split('*')[1]).ToList();
                }
                else
                {
                    fileNames = fileNames.Where(file => string.IsNullOrEmpty(keyword) || Path.GetExtension(file) == keyword.ToLower()).ToList();
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
