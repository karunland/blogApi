using BlogApi.Application.DTOs.File;
using BlogApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

public class FileController(FileService fileService) : BaseApiController
{
    [HttpPost]
    public async Task<FileResponse> UploadFile(UploadFileAsyncDto model)
    {
        return await fileService.UploadFileAsync(model);
    }

    [HttpGet]
    public async Task<IActionResult> Image([FromRoute] string imageUrl)
    {
        var file = await fileService.GetFileAsync(imageUrl);
        Response.Headers.Add("Content-Disposition", "inline; filename=" + file.FileName);

        return File(file.FileContents, file.ContentType);
    }
}