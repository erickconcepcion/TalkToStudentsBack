using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Markdig;
using TalkToStudentsBack.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TalkToStudentsBack.Services
{
    public class GithubService: IGithubService
    {
        public async Task<string> RepoMdToHtml(string owner, string repo, string path)
            => await RepoMdToHtml(await GetFileFromGithub(owner, repo, path));

        public Task<string> RepoMdToHtml(GithubResponse file)
        {
            if (!file.IsMd)
            {
                return null;
            }
            
            var md = file.StringValue;
            return Task.FromResult(Markdown.ToHtml(md));
        }
        
        public async Task<GithubResponse> GetFileFromGithub(string owner, string repo, string path)
        {
            var response = await GetGithubContent(owner, repo, path);
            return response.IsSuccessStatusCode ?  JsonSerializer.Deserialize<GithubResponse>(await response.Content.ReadAsStringAsync()) : null;
        }
        public Task<MemoryStream> RepoFileStream(GithubResponse file)
        {
            return Task.FromResult(new MemoryStream(file.ByteArray));
        }
        private Task<HttpResponseMessage> GetGithubContent(string owner, string repo, string path)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "request");
            return client.GetAsync($"https://api.github.com/repos/{owner}/{repo}/contents/{path}");
        }
    }
}