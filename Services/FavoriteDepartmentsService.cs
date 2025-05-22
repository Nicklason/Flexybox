using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

public class FavoriteDepartmentsService
{
  private readonly ProtectedLocalStorage storage;

  private const string STORE_KEY = "favorites";

  public FavoriteDepartmentsService(ProtectedLocalStorage storage)
  {
    this.storage = storage;
  }

  public async Task<HashSet<string>> GetLikedAsync()
  {
    var result = await storage.GetAsync<HashSet<string>>(STORE_KEY);
    if (result.Success && result.Value != null)
    {
      return result.Value;
    }
    return new HashSet<string>();
  }

  public async Task SaveLikedAsync(HashSet<string> liked)
  {
    await storage.SetAsync(STORE_KEY, liked);
  }
}
