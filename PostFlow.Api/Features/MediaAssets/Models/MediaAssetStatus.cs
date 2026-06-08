namespace PostFlow.Api.Features.MediaAssets.Models;

public class MediaAsset
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSizeBytes { get; set; }

    public string StoragePath { get; set; } = string.Empty;

    public DateTime UploadedAtUtc { get; set; }

    public MediaAssetStatus Status { get; set; }
}