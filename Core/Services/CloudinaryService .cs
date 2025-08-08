using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServicesAbstraction;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;



    public CloudinaryService(IConfiguration configuration)
    {
        var account = new Account(
              configuration["CloudinarySettings:CloudName"],
             configuration["CloudinarySettings:ApiKey"],
                 configuration["CloudinarySettings:ApiSecret"]
                 );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("A valid image file must be submitted.");

        try
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "courses/images"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Image upload failed: {uploadResult.Error?.Message}");

            
            return $"{uploadParams.Folder}/{uploadResult.PublicId}.{uploadResult.Format}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error loading image: {ex.Message}");
        }
    }

    public async Task<string> UploadVideoAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("A valid video file must be submitted.");

        try
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new VideoUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "courses/videos"
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception($"Video loading failed: {uploadResult.Error?.Message}");

     
            return $"{uploadParams.Folder}/{uploadResult.PublicId}.{uploadResult.Format}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error loading video: {ex.Message}");
        }
    }
    public async Task DeleteFileAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            throw new ArgumentException("A valid public ID must be provided.");

        try
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            if (result.Result != "ok")
                throw new Exception($"Failed to delete file: {result.Error?.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting file: {ex.Message}");
        }
    }
}
