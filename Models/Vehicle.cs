using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vehicle
    {
        // Variables propres à vehicule
        public int Id { get; set; }
        public string Immatriculation { get; set; }
        public int ColorId { get; set; }

        // Localization 

        public int LocalizationID { get; set; }
        public Localization Localization { get; set; }

        // Motorization

        public int MotorizationID { get; set; }
        public Motorization Motorization { get; set; }

        // Model

        public int ModelID { get; set; }
        public Model Model { get; set; }

        // State

        public int StateID { get; set; }
        public State State { get; set; }

        // Category 

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        // Brand 

        public int BrandID { get; set; }
        public Brand Brand { get; set; }

        // Rents

        public List<Rent>? Rents { get; set; }

        // Picture

        public string PictureURL { get; set; }

        public override string ToString()
        {
            return $"{this.Category.Label}, {this.Motorization.Label}, {this.Category.Label} places, {this.Brand.Label}, {this.Model.Label}, {this.Model.Year}";
        }
    }
}
