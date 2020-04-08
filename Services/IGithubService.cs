using System.IO;
using System.Threading.Tasks;
using TalkToStudentsBack.Models;

namespace TalkToStudentsBack.Services
{
    public interface IGithubService
    {
         Task<MemoryStream> RepoFileStream(GithubResponse file);

        /// <summary>
        /// Gets .md file from github repo and converts to html.
        /// </summary>
        /// <param name="file">Deserialize github content</param>
        /// <returns>Parsed html</returns>
        Task<string> RepoMdToHtml(GithubResponse file);

        /// <summary>
        /// Gets .md file from github repo and converts to html.
        /// </summary>
        /// <param name="owner">Repo owner name.</param>
        /// <param name="repo">Repo name</param>
        /// <param name="path">Github file path. Ex: if
        ///  https://github.com/me/myrepo/blob/master/readme.md, path is readme.md</param>
        /// <returns>Parsed html</returns>
        Task<string> RepoMdToHtml(string owner, string repo, string path);


        /// <summary>
        /// Gets Deserialized github content.
        /// </summary>
        /// <param name="owner">Repo owner name.</param>
        /// <param name="repo">Repo name</param>
        /// <param name="path">Github file path. Ex: if
        ///  https://github.com/me/myrepo/blob/master/readme.md, path is readme.md</param>
        /// <returns>Deserialize github content</returns>
        Task<GithubResponse> GetFileFromGithub(string owner, string repo, string path);
    }
}