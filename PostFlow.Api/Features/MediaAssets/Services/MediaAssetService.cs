using Microsoft.AspNetCore.Http;
using PostFlow.Api.Features.MediaAssets.Contracts;
using PostFlow.Api.Features.MediaAssets.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;

namespace PostFlow.Api.Features.MediaAssets.Services;

public class MediaAssetService : IMediaAssetService
{
    private readonly IDocumentStore _store ;

    public MediaAssetService(IDocumentStore store)
    {
        _store = store;
    }
    public async Task<MediaAssetResponse> UploadAsync(IFormFile file)
    {
        if (file.Length == 0)
        {
            throw new ArgumentException("Uploaded file cannot be empty.");
        }


        var mediaAsset = new MediaAsset
        {
            FileName = file.FileName,
            ContentType = file.ContentType,
            FileSizeBytes = file.Length,
            StoragePath = $"uploads/media/{Guid.NewGuid()}-{file.FileName}",
            UploadedAtUtc = DateTime.UtcNow,
            Status = MediaAssetStatus.Uploaded
        };

        using var session = _store.OpenAsyncSession();

        await session.StoreAsync(mediaAsset);
        await session.SaveChangesAsync();

        return ToResponse(mediaAsset);

    }

    public async Task<List<MediaAssetResponse>> GetAllAsync()
    {
        using var session = _store.OpenAsyncSession();

        var mediaAssets = await session
        .Query<MediaAsset>()
        .ToListAsync();

        return mediaAssets
        .Select(ToResponse)
        .ToList();
    }

    public async Task<MediaAssetResponse?> GetByIdAsync(string id)
    {
        using var session = _store.OpenAsyncSession();

        var mediaAsset = await session.LoadAsync<MediaAsset>(id);

        if (mediaAsset is null)
        {
            return null;
        }

        return ToResponse(mediaAsset);
    }

    private static MediaAssetResponse ToResponse(MediaAsset mediaAsset)
    {
        return new MediaAssetResponse
        {
            FileName = mediaAsset.FileName,
            ContentType = mediaAsset.ContentType,
            FileSizeBytes = mediaAsset.FileSizeBytes,
            StoragePath = mediaAsset.StoragePath,
            UploadedAtUtc = mediaAsset.UploadedAtUtc,
            Status = mediaAsset.Status
        };
    }
}