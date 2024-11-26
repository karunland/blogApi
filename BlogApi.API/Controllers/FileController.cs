using BlogApi.Application.DTOs.File;
using BlogApi.Application.Services;
using BlogApi.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

public class FileController(FileRepo fileRepo) : BaseApiController
{
    [HttpPost]
    public async Task<FileResponse> UploadFile(UploadFileAsyncDto model)
    {
        return await fileRepo.UploadFileAsync(model);
    }

    [HttpGet]
    public async Task<IActionResult> Image([FromRoute] string imageUrl)
    {
        var file = await fileRepo.GetFileAsync(imageUrl);
        Response.Headers.Append("Content-Disposition", "inline; filename=" + file.FileName);

        return File(file.FileContents, file.ContentType);
    }
}