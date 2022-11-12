using System.ComponentModel.DataAnnotations;

namespace ChuborekekCRUD.Enum
{
    public enum DogBreed
    {
        Aspin,
        Shitzu,
        Labrador,
        [Display(Name = "Golden Retriever")]
        GoldenRetriever,
        Poodle,
        Unknown
    }
}
