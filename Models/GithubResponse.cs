using System;
using System.Linq;
using System.Text.Json.Serialization;
namespace TalkToStudentsBack.Models
{
    public class GithubResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("path")]
        public string Path { get; set; }

        [JsonPropertyName("sha")]
        public string Sha { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("encoding")]
        public string Encoding { get; set; }

        public byte[] ByteArray => Convert.FromBase64CharArray(Content.ToCharArray(), 0, Content.Length);
        public string StringValue => System.Text.Encoding.UTF8.GetString(ByteArray);
        public bool IsMd => Name.Split(".").LastOrDefault().ToLower() == "md";
    }
}