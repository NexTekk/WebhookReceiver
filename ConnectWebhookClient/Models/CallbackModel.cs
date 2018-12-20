using System;

namespace ConnectWebhookClient.Models
{
    public class WebhookPayload
    {
        public WebhookEventModel Event { get; set; }

        public RequestModel Request { get; set; }

        public ArtifactModel Artifact { get; set; }
    }

    public class ArtifactPayload
    {
        public WebhookEventModel Event { get; set; }

        public ArtifactModel Artifact { get; set; }
    }

    public class WebhookEventModel
    {
        public string Object { get; set; }

        public string Action { get; set; }

        public Person ActionedBy { get; set; }

        public DateTime TimestampUtc { get; set; }

    }

    public class Person
    {
        public string Ppi { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }

    public class RequestModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Identifier { get; set; }

        public string Status { get; set; }

        public TitledIdModel Site { get; set; }

        public TitledIdModel Engagement { get; set; }

        public Person PrimaryOwner { get; set; }
    }

    public class TitledIdModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
    }

    public class ArtifactModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public long FileSizeInBytes { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string DownloadUrl { get; set; }

        public string DocumentType { get; set; }

        public RequestModel Request { get; set; }
    }
}
