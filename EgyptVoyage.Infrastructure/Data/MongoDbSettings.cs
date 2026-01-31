using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EgyptVoyage.Infrastructure.Data;

/// <summary>
/// MongoDB configuration settings
/// </summary>
public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;

    // Collection Names
    public string HotelsCollectionName { get; set; } = "Hotels";
    public string RestaurantsCollectionName { get; set; } = "Restaurants";
    public string LandmarksCollectionName { get; set; } = "Landmarks";
    public string ProgramsCollectionName { get; set; } = "Programs";
    public string TouristsCollectionName { get; set; } = "Tourists";
    public string ClerksCollectionName { get; set; } = "Clerks";
    public string ReviewsCollectionName { get; set; } = "Reviews";
    public string FavoriteListsCollectionName { get; set; } = "FavoriteLists";
}