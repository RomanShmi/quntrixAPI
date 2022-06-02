using ConferenceDTO;
using Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Front.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IApiClient _apiClient;
        public string Term { get; set; } = String.Empty;
        public List<SearchResult> SearchResults { get; set; } = new();

        public SearchModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task OnGetAsync(string term)
        {
            Term = term;
            if (!string.IsNullOrWhiteSpace(term))
            {
                SearchResults = await _apiClient.SearchAsync(term);
            }
        }



    }
}
