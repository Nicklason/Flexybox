using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class LikedRestaurantsService
{
  private readonly ProtectedLocalStorage storage;

  private const string LIKED_STORE_KEY = "liked";

  public LikedRestaurantsService(ProtectedLocalStorage storage)
  {
    this.storage = storage;
  }

  public async Task<HashSet<string>> GetLikedAsync()
  {
    var result = await storage.GetAsync<HashSet<string>>(LIKED_STORE_KEY);
    if (result.Success && result.Value != null)
    {
      return result.Value;
    }
    return new HashSet<string>();
  }

  public async Task SaveLikedAsync(HashSet<string> liked)
  {
    await storage.SetAsync(LIKED_STORE_KEY, liked);
  }
}
