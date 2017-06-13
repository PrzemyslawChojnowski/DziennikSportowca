using DziennikSportowca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if(!context.MuscleParts.Any())
            {
                var muscleParts = new MusclePart[]
                {
                    new MusclePart { Description = "Barki"},
                    new MusclePart { Description = "Klatka piersiowa"},
                    new MusclePart { Description = "Plecy"},
                    new MusclePart { Description = "Ramiona"},
                    new MusclePart { Description = "Przedramiona"},
                    new MusclePart { Description = "Brzuch"},
                    new MusclePart { Description = "Uda i pośladki"},
                    new MusclePart { Description = "Łydki"}
                };
                foreach(MusclePart mp in muscleParts)
                {
                    context.MuscleParts.Add(mp);
                }
                context.SaveChanges();
            }

            if(!context.Exercises.Any())
            {
                var exercises = new Exercise[]
                {
                    new Exercise{Name = "Wyciskanie sztangi sprzed głowy"},
                    new Exercise{Name = "Wyciskanie sztangi zza głowy"},
                    new Exercise{Name = "Wyciskanie hantli"},
                    new Exercise{Name = "Arnoldki"},
                    new Exercise{Name = "Unoszenie hantli bokiem w górę"},
                    new Exercise{Name = "Unoszenie hantli w opadzie tułowia"},
                    new Exercise{Name = "Podciąganie sztangi wzdłuż tułowia"},
                    new Exercise{Name = "Podciąganie hantli wzdłuż tułowia"},
                    new Exercise{Name = "Unoszenie ramion w przód ze sztangą"},
                    new Exercise{Name = "Unoszenie ramion w przód ze sztangielkami"},
                    new Exercise{Name = "Unoszenie ramion z hantlami w leżeniu"},
                    new Exercise{Name = "Unoszenie ramion w przód z linkami wyciągu"},
                    new Exercise{Name = "Unoszenie ramion bokiem w górę z linkami wyciągu"},
                    new Exercise{Name = "Unoszenie ramion bokiem w górę w opadzie tułowia z linkami wyciągu"},
                    new Exercise{Name = "Odwrotne rozpiętki"},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce poziomej"},
                    new Exercise{Name = "Wyciskanie hantli w leżeniu na ławce poziomej"},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce skośnej - głową  w górę"},
                    new Exercise{Name = "Wyciskanie hantli w leżeniu na ławce skośnej - głową w górę"},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce skośnej - głową w dół"},
                    new Exercise{Name = "Wyciskanie hantli w leżeniu na ławce skośnej - głową w dół"},
                    new Exercise{Name = "Rozpiętki z hantlami w leżeniu na ławce poziomej"},
                    new Exercise{Name = "Rozpiętki z hantlami w leżeniu na ławce skośnej - głową do góry"},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce poziomej wąskim chwytem"},
                    new Exercise{Name = "Przenoszenie hantli w leżeniu w poprzek ławki poziomej"},
                    new Exercise{Name = "Pompki na poręczach"},
                    new Exercise{Name = "Rozpiętki w siadzie na maszynie"},
                    new Exercise{Name = "Krzyżowanie linek wyciągu w staniu"},
                    new Exercise{Name = "Wyciskania poziome w siadzie na maszynie"},
                    new Exercise{Name = "Podciąganie na drążku szerokim uchwytem"},
                    new Exercise{Name = "Podciąganie na drążku w uchwycie neutralnym"},
                    new Exercise{Name = "Podciąganie na drążku podchwytem"},
                    new Exercise{Name = "Podciąganie sztangi w opadzie (wiosłowanie)"},
                    new Exercise{Name = "Podciąganie sztangielki w opadzie (wiosłowanie)"},
                    new Exercise{Name = "Podciąganie końca sztangi w opadzie"},
                    new Exercise{Name = "Przyciąganie linki wyciągu dolnego w siadzie płaskim"},
                    new Exercise{Name = "Przyciąganie linki wyciągu górnego w siadzie"},
                    new Exercise{Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie szerokim uchwytem (nachwyt)"},
                    new Exercise{Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie podchwytem"},
                    new Exercise{Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie uchwyt neutralny"},
                    new Exercise{Name = "Przenoszenie sztangi w leżeniu na ławce poziomej"},
                    new Exercise{Name = "Podciąganie (wiosłowanie) w leżeniu na ławeczce poziomej"},
                    new Exercise{Name = "Skłony ze sztangą trzymaną na karku (tzw. \"dzień dobry\")"},
                    new Exercise{Name = "Unoszenie tułowia z opadu"},
                    new Exercise{Name = "Martwy ciąg"},
                    new Exercise{Name = "Martwy ciąg na prostych nogach"},
                    new Exercise{Name = "Szrugsy"},
                    new Exercise{Name = "Uginanie ramion ze sztangą stojąc podchwytem"},
                    new Exercise{Name = "Uginanie ramion z hantlami stojąc podchwytem z supinacją nadgarstka"},
                    new Exercise{Name = "Uginanie ramion z hantlami stojąc (uchwyt młotkowy)"},
                    new Exercise{Name = "Uginanie ramion ze sztangą na modlitewniku"},
                    new Exercise{Name = "Uginanie ramienia z hantlą na modlitewniku"},
                    new Exercise{Name = "Uginanie ramion z hantlami w siadzie na ławce skośnej z supinacją nadgarstka"},
                    new Exercise{Name = "Uginanie ramienia z hantlą w siadzie (w podporze o kolano)"},
                    new Exercise{Name = "Uginanie ramion z rączką wyciągu podchwytem stojąc"},
                    new Exercise{Name = "Uginanie ramion ze sztanga nachwytem stojąc"},
                    new Exercise{Name = "Uginanie ramion ze sztanga nachwytem na modlitewniku"},
                    new Exercise{Name = "Uginanie nadgarstków podchwytem w siadzie"},
                    new Exercise{Name = "Uginanie nadgarstków nachwytem w siadzie"},
                    new Exercise{Name = "Prostowanie ramion na wyciągu stojąc"},
                    new Exercise{Name = "Wyciskanie francuskie sztangi w siadzie"},
                    new Exercise{Name = "Wyciskanie francuskie jednorącz hantli w siadzie"},
                    new Exercise{Name = "Wyciskanie francuskie sztangi w leżeniu"},
                    new Exercise{Name = "Wyciskanie francuskie hantli w leżeniu"},
                    new Exercise{Name = "Prostownie ramienia z hantlą w opadzie tułowia"},
                    new Exercise{Name = "Prostowanie ramion na wyciągu w płaszczyźne poziomej stojąc"},
                    new Exercise{Name = "Prostowanie ramion na wyciągu w płaszczyźnie poziomej w podporze"},
                    new Exercise{Name = "Pompki na poręczach"},
                    new Exercise{Name = "Pompki w podporze tyłem"},
                    new Exercise{Name = "Prostowanie ramienia podchwytem na wyciągu stojąc"},
                    new Exercise{Name = "Wyciskanie w leżeniu na ławce poziomej wąskim uchwytem"}
                };
                foreach(Exercise e in exercises)
                {
                    context.Exercises.Add(e);
                }
                context.SaveChanges();
            }           
        }
    }
}
