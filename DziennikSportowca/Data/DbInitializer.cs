using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Bcpg.Attr;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DziennikSportowca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace DziennikSportowca.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if(!context.ActivityTypes.Any())
                {
                    List<ActivityType> activities = new List<ActivityType>()
                    {
                        new ActivityType { Description = "Ćwiczenia siłowe" },
                        new ActivityType { Description = "Ćwiczenia wytrzymałościowe" },
                        new ActivityType { Description = "Sporty grupowe" }
                    };
                    context.ActivityTypes.AddRange(activities);
                    context.SaveChanges();
                }

                if (!context.MuscleParts.Any())
                {
                    MusclePart[] muscleParts = new MusclePart[]
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
                    context.MuscleParts.AddRange(muscleParts);
                    context.SaveChanges();
                }

                //if(!context.ExerciseInstructions.Any())
                //{
                //    List<ExerciseInstruction> instructions = new List<ExerciseInstruction>()
                //    {
                //        new ExerciseInstruction{ExerciseId = }
                //    }
                //}

                if (!context.Exercises.Any())
                {
                    ActivityType strengthExercises = context.ActivityTypes.FirstOrDefault(x => x.Description == "Ćwiczenia siłowe");
                    ActivityType enduranceExercises = context.ActivityTypes.FirstOrDefault(x => x.Description == "Ćwiczenia wytrzymałościowe");
                    ActivityType groupSports = context.ActivityTypes.FirstOrDefault(x => x.Description == "Sporty grupowe");
                    //MusclePart barki = context.MuscleParts.FirstOrDefault(x => x.Description == "Barki");

                    List<Exercise> exercises = new List<Exercise>
                    {
                    new Exercise{Name = "Wyciskanie sztangi sprzed głowy", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie sztangi zza głowy", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie hantli", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Arnoldki", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie hantli bokiem w górę", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie hantli w opadzie tułowia", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie sztangi wzdłuż tułowia", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie hantli wzdłuż tułowia", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie ramion w przód ze sztangą", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie ramion w przód ze sztangielkami", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie ramion z hantlami w leżeniu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie ramion w przód z linkami wyciągu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie ramion bokiem w górę z linkami wyciągu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie ramion bokiem w górę w opadzie tułowia z linkami wyciągu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Odwrotne rozpiętki", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce poziomej", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie hantli w leżeniu na ławce poziomej", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce skośnej - głową  w górę", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie hantli w leżeniu na ławce skośnej - głową w górę", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce skośnej - głową w dół", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie hantli w leżeniu na ławce skośnej - głową w dół", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Rozpiętki z hantlami w leżeniu na ławce poziomej", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Rozpiętki z hantlami w leżeniu na ławce skośnej - głową do góry", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie sztangi w leżeniu na ławce poziomej wąskim chwytem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Przenoszenie hantli w leżeniu w poprzek ławki poziomej", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Pompki na poręczach", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Rozpiętki w siadzie na maszynie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Krzyżowanie linek wyciągu w staniu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskania poziome w siadzie na maszynie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie na drążku szerokim uchwytem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie na drążku w uchwycie neutralnym", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie na drążku podchwytem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie sztangi w opadzie (wiosłowanie)", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie sztangielki w opadzie (wiosłowanie)", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie końca sztangi w opadzie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Przyciąganie linki wyciągu dolnego w siadzie płaskim", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Przyciąganie linki wyciągu górnego w siadzie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie szerokim uchwytem (nachwyt)", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie podchwytem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Ściąganie drążka/rączki wyciągu górnego w siadzie uchwyt neutralny", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Przenoszenie sztangi w leżeniu na ławce poziomej", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Podciąganie (wiosłowanie) w leżeniu na ławeczce poziomej", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Skłony ze sztangą trzymaną na karku (tzw. \"dzień dobry\")", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Unoszenie tułowia z opadu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Martwy ciąg", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Martwy ciąg na prostych nogach", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Szrugsy", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion ze sztangą stojąc podchwytem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion z hantlami stojąc podchwytem z supinacją nadgarstka", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion z hantlami stojąc (uchwyt młotkowy)", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion ze sztangą na modlitewniku", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramienia z hantlą na modlitewniku", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion z hantlami w siadzie na ławce skośnej z supinacją nadgarstka", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramienia z hantlą w siadzie (w podporze o kolano)", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion z rączką wyciągu podchwytem stojąc", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion ze sztanga nachwytem stojąc", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie ramion ze sztanga nachwytem na modlitewniku", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie nadgarstków podchwytem w siadzie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Uginanie nadgarstków nachwytem w siadzie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Prostowanie ramion na wyciągu stojąc", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie francuskie sztangi w siadzie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie francuskie jednorącz hantli w siadzie", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie francuskie sztangi w leżeniu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie francuskie hantli w leżeniu", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Prostownie ramienia z hantlą w opadzie tułowia", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Prostowanie ramion na wyciągu w płaszczyźne poziomej stojąc", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Prostowanie ramion na wyciągu w płaszczyźnie poziomej w podporze", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Pompki na poręczach", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Pompki w podporze tyłem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Prostowanie ramienia podchwytem na wyciągu stojąc", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Wyciskanie w leżeniu na ławce poziomej wąskim uchwytem", ActivityType = strengthExercises, ActivityTypeId = strengthExercises.Id},
                    new Exercise{Name = "Bieganie", ActivityType = enduranceExercises, ActivityTypeId = enduranceExercises.Id},
                    new Exercise{Name = "Jazda na rowerze", ActivityType = enduranceExercises, ActivityTypeId = enduranceExercises.Id},
                    new Exercise{Name = "Gra w piłkę nożną", ActivityType = groupSports, ActivityTypeId = groupSports.Id},
                    new Exercise{Name = "Gra w piłkę siatkową", ActivityType = groupSports, ActivityTypeId = groupSports.Id}
                    };
                    context.Exercises.AddRange(exercises);

                    

                    context.SaveChanges();
                }

                if(!context.ExerciseInstructions.Any())
                {
                    List<ExerciseInstruction> instructions = new List<ExerciseInstruction>()
                    {
                        new ExerciseInstruction{ExerciseId = 1, Instructions = "Opis wykonania ćwiczenia"}
                    };
                    context.AddRange(instructions);
                    //var exercise = context.Exercises.FirstOrDefault(x => x.Id == 1);
                    //exercise.ExerciseInstructionId = 1;
                    //context.Exercises.Update(exercise);
                    context.SaveChanges();
                }

                if (!context.ExerciseInstructionPhotos.Any())
                {
                    byte[] selena;
                    byte[] selena2;

                    MemoryStream ms = new MemoryStream();
                    using (FileStream stream = new FileStream("C:\\Users\\przem\\Desktop\\Nowy folder\\selena.jpg", FileMode.Open))
                    {
                        stream.CopyTo(ms);
                        selena = new byte[stream.Length];
                        selena = ms.ToArray();
                    }
                    MemoryStream ms2 = new MemoryStream();
                    using (FileStream stream = new FileStream("C:\\Users\\przem\\Desktop\\Nowy folder\\selena2.jpg", FileMode.Open))
                    {
                        stream.CopyTo(ms2);
                        selena2 = new byte[stream.Length];
                        selena2 = ms2.ToArray();
                    }

                    List<ExerciseInstructionPhoto> photos = new List<ExerciseInstructionPhoto>()
                    {
                        new ExerciseInstructionPhoto{PhotoTitle = "Selena Gomez", ExerciseInstructionId = 2, Content = selena},
                        new ExerciseInstructionPhoto{PhotoTitle = "Selena Gomez 2", ExerciseInstructionId = 2, Content = selena2}
                    };

                    context.ExerciseInstructionPhotos.AddRange(photos);

                    context.SaveChanges();
                }

                if (!context.MusclePartExercises.Any())
                {
                    MusclePartExercise[] musclePartExercises = new MusclePartExercise[]
                    {
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 1},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 51},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 50},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 49},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 48},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 47},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 46},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 52},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 45},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 43},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 42},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 41},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 40},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 39},
                    new MusclePartExercise{MuscePartId = 1, ExerciseId = 38},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 44},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 54},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 62},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 55},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 69},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 68},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 67},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 66},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 65},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 64},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 70},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 63},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 61},
                    new MusclePartExercise{MuscePartId = 2, ExerciseId = 60}
                    };
                    context.MusclePartExercises.AddRange(musclePartExercises);
                    context.SaveChanges();
                }

                if (!context.FoodProductsTypes.Any())
                {
                    List<FoodProductType> FoodProductsTypes = new List<FoodProductType>
                    {
                        new FoodProductType { Description = "Nabiał i jaja" },
                        new FoodProductType { Description = "Produkty zbożowe" },
                        new FoodProductType { Description = "Mięso, wędliny i dania mięsne" },
                        new FoodProductType { Description = "Słodycze i przekąski" },
                        new FoodProductType { Description = "Bakalie i nasiona" },
                        new FoodProductType { Description = "Napoje" },
                        new FoodProductType { Description = "Owoce i ich przetwory" },
                        new FoodProductType { Description = "Produkty gotowe" },
                        new FoodProductType { Description = "Ryby i dania rybne" },
                        new FoodProductType { Description = "Tłuszcze" },
                        new FoodProductType { Description = "Warzywa i ich przetwory" },
                    };
                    context.FoodProductsTypes.AddRange(FoodProductsTypes);
                    context.SaveChanges();
                }

                if (!context.FoodProducts.Any())
                {
                    FoodProductType milkProducts = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Nabiał i jaja");
                    FoodProductType grainProducts = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Produkty zbożowe");
                    FoodProductType meatProducts = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Mięso, wędliny i dania mięsne");
                    FoodProductType sweets = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Słodycze i przekąski");
                    FoodProductType nutsAndSeeds = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Bakalie i nasiona");
                    FoodProductType drinks = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Napoje");
                    FoodProductType fruits = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Owoce i ich przetwory");
                    FoodProductType preparedProducts = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Produkty gotowe");
                    FoodProductType fishes = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Ryby i dania rybne");
                    FoodProductType fats = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Tłuszcze");
                    FoodProductType vegetables = context.FoodProductsTypes.SingleOrDefault(x => x.Description == "Warzywa i ich przetwory");

                    List<FoodProduct> foodProducts = new List<FoodProduct>()
                    {
                        new FoodProduct { Description = "Melko 1%", Carbohydrate = 5.0, Energy = 42, Fat = 1, Measurement = Measurement.Weight, Protein = 3.4, Type = milkProducts },
                        new FoodProduct { Description = "Jajko", Carbohydrate = 1.1, Energy = 155, Fat = 11, Measurement = Measurement.Weight, Protein = 13, Type = milkProducts},
                        new FoodProduct { Description = "Pierś z kurczaka", Carbohydrate = 0, Energy = 99, Fat = 1.3, Measurement = Measurement.Weight, Protein = 21.5, Type = meatProducts},
                        new FoodProduct { Description = "Bagietka", Carbohydrate = 52.8, Energy = 265, Fat = 0.7, Measurement = Measurement.Weight, Protein = 7.9, Type = grainProducts},
                        new FoodProduct { Description = "Bułka z dynią", Carbohydrate = 46.4, Energy = 326, Fat = 10.3, Measurement = Measurement.Weight, Protein = 11.8, Type = grainProducts},
                        new FoodProduct { Description = "Ciastka \"Jeżyki\"", Carbohydrate = 58.2, Energy = 481, Fat = 24.6, Measurement = Measurement.Weight, Protein = 6.6, Type = sweets},
                        new FoodProduct { Description = "Cukierki \"Kukułki\"", Carbohydrate = 80, Energy = 400, Fat = 5.9, Measurement = Measurement.Weight, Protein = 1.7, Type = sweets},
                        new FoodProduct { Description = "Białko jaja kurzego", Carbohydrate = 0.7, Energy = 48, Fat = 0.2, Measurement = Measurement.Weight, Protein = 10.9, Type = milkProducts},
                        new FoodProduct { Description = "Rodzynki", Carbohydrate = 71.2, Energy = 277, Fat = 0.5, Measurement = Measurement.Weight, Protein = 2.3, Type = nutsAndSeeds},
                        new FoodProduct { Description = "Daktyle", Carbohydrate = 75, Energy = 277, Fat = 0.2, Measurement = Measurement.Weight, Protein = 75, Type = nutsAndSeeds},
                        new FoodProduct { Description = "Fistaszki", Carbohydrate = 19.2, Energy = 560, Fat = 46.1, Measurement = Measurement.Weight, Protein = 25.7, Type = nutsAndSeeds},
                        new FoodProduct { Description = "Kawa rozpuszczalna \"Nescafe 3w1\"", Carbohydrate = 6.7, Energy = 38, Fat = 1, Measurement = Measurement.Weight, Protein = 0.4, Type = drinks},
                        new FoodProduct { Description = "Piwo", Carbohydrate = 3.8, Energy = 49, Fat = 0, Measurement = Measurement.Weight, Protein = 0, Type = drinks},
                        new FoodProduct { Description = "Herbata owocowa rozpuszczalna", Carbohydrate = 97.1, Energy = 393, Fat = 0, Measurement = Measurement.Weight, Protein = 0, Type = drinks},
                        new FoodProduct { Description = "Ananas", Carbohydrate = 13.1, Energy = 50, Fat = 0.1, Measurement = Measurement.Weight, Protein = 0.5, Type = fruits},
                        new FoodProduct { Description = "Oliwki zielone", Carbohydrate = 4.1, Energy = 125, Fat = 12.7, Measurement = Measurement.Weight, Protein = 1.4, Type = fruits},
                        new FoodProduct { Description = "Brzoskwinia", Carbohydrate = 9.5, Energy = 39, Fat = 0.3, Measurement = Measurement.Weight, Protein = 0.9, Type = fruits},
                        new FoodProduct { Description = "Paszteciki z grzybami", Carbohydrate = 36.1, Energy = 310, Fat = 14.8, Measurement = Measurement.Weight, Protein = 8.7, Type = preparedProducts},
                        new FoodProduct { Description = "Budyń czekoladowy", Carbohydrate = 16.4, Energy = 95, Fat = 1.9, Measurement = Measurement.Weight, Protein = 3.2, Type = preparedProducts},
                        new FoodProduct { Description = "Pierogi z truskawkami", Carbohydrate = 30.3, Energy = 146, Fat = 1.4, Measurement = Measurement.Weight, Protein = 2.5, Type = preparedProducts},
                        new FoodProduct { Description = "Dorsz wędzony", Carbohydrate = 0, Energy = 94, Fat = 0.5, Measurement = Measurement.Weight, Protein = 22.1, Type = fishes},
                        new FoodProduct { Description = "Krewetki tygrysie", Carbohydrate = 1, Energy = 92, Fat = 0, Measurement = Measurement.Weight, Protein = 22, Type = fishes},
                        new FoodProduct { Description = "Filet mrożony z pangi", Carbohydrate = 0, Energy = 53, Fat = 0.6, Measurement = Measurement.Weight, Protein = 11.9, Type = fishes},
                        new FoodProduct { Description = "Margaryna do pieczenia", Carbohydrate = 0.5, Energy = 680, Fat = 75, Measurement = Measurement.Weight, Protein = 0.5, Type = fats},
                        new FoodProduct { Description = "Olej kokosowy tłoczony na zimno", Carbohydrate = 0, Energy = 900, Fat = 100, Measurement = Measurement.Weight, Protein = 0, Type = fats},
                        new FoodProduct { Description = "Olej sezamowy", Carbohydrate = 0, Energy = 900, Fat = 100, Measurement = Measurement.Weight, Protein = 0, Type = fats},
                        new FoodProduct { Description = "Ogórki konserwowe", Carbohydrate = 4.9, Energy = 23, Fat = 0.1, Measurement = Measurement.Weight, Protein = 0.4, Type = vegetables},
                        new FoodProduct { Description = "Bakłażan", Carbohydrate = 6.3, Energy = 21, Fat = 0.1, Measurement = Measurement.Weight, Protein = 1.1, Type = vegetables},
                        new FoodProduct { Description = "Cukinia", Carbohydrate = 3.1, Energy = 17, Fat = 0.3, Measurement = Measurement.Weight, Protein = 1.2, Type = vegetables}
                    };
                    context.FoodProducts.AddRange(foodProducts);
                    context.SaveChanges();
                }                   
                
            }
        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>())
            {
                string[] roles = new[] { "Admin", "User" };

                IdentityResult roleResult;

                foreach (var role in roles)
                {
                    var roleExist = await roleManager.RoleExistsAsync(role);
                    if (!roleExist)
                        roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }

                var powerUser = new ApplicationUser()
                {
                    Email = configuration.GetSection("PowerUser")["AdminEmail"],
                    Name = configuration.GetSection("PowerUser")["AdminName"],
                    UserName = configuration.GetSection("PowerUser")["AdminEmail"],
                    Surname = configuration.GetSection("PowerUser")["AdminSurname"],
                    Gender = Gender.Man
                };

                var manager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string powerUserPassword = configuration.GetSection("PowerUser")["AdminPassword"];
                var user = await manager.FindByEmailAsync(configuration.GetSection("PowerUser")["AdminEmail"]);

                if (user == null) 
                {
                    var createPowerUser = await manager.CreateAsync(powerUser, powerUserPassword);

                    if (createPowerUser.Succeeded)
                        await manager.AddToRoleAsync(powerUser, "Admin");
                }
            }
        }
    }
}
