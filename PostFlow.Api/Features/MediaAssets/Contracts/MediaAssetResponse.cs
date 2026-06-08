using PostFlow.Api.Features.MediaAssets.Models;

namespace PostFlow.Api.Features.MediaAssets.Contracts;

public class MediaAssetResponse
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSizeBytes { get; set; }

    public string StoragePath { get; set; } = string.Empty;

    public DateTime UploadedAtUtc { get; set; }

    public MediaAssetStatus Status { get; set; }
}