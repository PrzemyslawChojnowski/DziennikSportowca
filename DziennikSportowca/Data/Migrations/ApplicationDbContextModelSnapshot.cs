using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DziennikSportowca.Data;
using DziennikSportowca.Models;

namespace DziennikSportowca.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DziennikSportowca.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<byte[]>("ProfilePicture");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DziennikSportowca.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Dish");
                });

            modelBuilder.Entity("DziennikSportowca.Models.DishFoodProduct", b =>
                {
                    b.Property<int>("DishId");

                    b.Property<int>("FoodProductId");

                    b.HasKey("DishId", "FoodProductId");

                    b.HasIndex("FoodProductId");

                    b.ToTable("DishFoodProduct");
                });

            modelBuilder.Entity("DziennikSportowca.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("DziennikSportowca.Models.ExerciseWeight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("UserTrainingExerciseResultId");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("UserTrainingExerciseResultId");

                    b.ToTable("ExerciseWeight");
                });

            modelBuilder.Entity("DziennikSportowca.Models.FoodProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Carbohydrate");

                    b.Property<string>("Description");

                    b.Property<double>("Energy");

                    b.Property<double>("Fat");

                    b.Property<int>("Measurement");

                    b.Property<double>("Protein");

                    b.HasKey("Id");

                    b.ToTable("FoodProduct");
                });

            modelBuilder.Entity("DziennikSportowca.Models.MusclePart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("MusclePart");
                });

            modelBuilder.Entity("DziennikSportowca.Models.MusclePartExercise", b =>
                {
                    b.Property<int>("ExerciseId");

                    b.Property<int>("MuscePartId");

                    b.HasKey("ExerciseId", "MuscePartId");

                    b.HasIndex("MuscePartId");

                    b.ToTable("MusclePartExercise");
                });

            modelBuilder.Entity("DziennikSportowca.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("PhotoContent");

                    b.Property<int>("UserFigureId");

                    b.HasKey("Id");

                    b.HasIndex("UserFigureId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("DziennikSportowca.Models.TrainingPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("TrainingPlan");
                });

            modelBuilder.Entity("DziennikSportowca.Models.TrainingPlanExercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ExerciseId");

                    b.Property<int>("RepsNo");

                    b.Property<int>("SeriesNo");

                    b.Property<int>("TrainingPlanId");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("TrainingPlanId");

                    b.ToTable("TrainingPlanExercise");
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserFigure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BicepsCircumference");

                    b.Property<double>("BodyFat");

                    b.Property<double>("ChestCircumference");

                    b.Property<DateTime>("Date");

                    b.Property<double>("HipCircumference");

                    b.Property<double>("ShouldersCircumference");

                    b.Property<double>("ThighCircumference");

                    b.Property<double>("TricepsCircumference");

                    b.Property<string>("UserId");

                    b.Property<double>("WaistCircumference");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFigure");
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserFriend", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("FriendId");

                    b.Property<int>("FriendshipStatus");

                    b.HasKey("UserId", "FriendId");

                    b.HasIndex("FriendId");

                    b.ToTable("UserFriend");
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndDateTime");

                    b.Property<DateTime>("StartDateTime");

                    b.Property<int>("TrainingId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingId");

                    b.ToTable("UserTraining");
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserTrainingExerciseResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RepsNo");

                    b.Property<int>("SeriesNo");

                    b.Property<int>("TrainingPlanExerciseId");

                    b.Property<int>("UserTrainingId");

                    b.HasKey("Id");

                    b.HasIndex("TrainingPlanExerciseId");

                    b.HasIndex("UserTrainingId");

                    b.ToTable("UserTrainingExerciseResult");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DziennikSportowca.Models.Dish", b =>
                {
                    b.HasOne("DziennikSportowca.Models.ApplicationUser", "User")
                        .WithMany("Dishes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DziennikSportowca.Models.DishFoodProduct", b =>
                {
                    b.HasOne("DziennikSportowca.Models.Dish", "Dish")
                        .WithMany("FoodProducts")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DziennikSportowca.Models.FoodProduct", "FoodProduct")
                        .WithMany("Dishes")
                        .HasForeignKey("FoodProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.ExerciseWeight", b =>
                {
                    b.HasOne("DziennikSportowca.Models.UserTrainingExerciseResult", "UserTrainingExerciseResult")
                        .WithMany("Weights")
                        .HasForeignKey("UserTrainingExerciseResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.MusclePartExercise", b =>
                {
                    b.HasOne("DziennikSportowca.Models.Exercise", "Exercise")
                        .WithMany("MuscleParts")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DziennikSportowca.Models.MusclePart", "MusclePart")
                        .WithMany("Exercises")
                        .HasForeignKey("MuscePartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.Photo", b =>
                {
                    b.HasOne("DziennikSportowca.Models.UserFigure", "UserFigure")
                        .WithMany("Photos")
                        .HasForeignKey("UserFigureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.TrainingPlan", b =>
                {
                    b.HasOne("DziennikSportowca.Models.ApplicationUser", "User")
                        .WithMany("TrainingPlans")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DziennikSportowca.Models.TrainingPlanExercise", b =>
                {
                    b.HasOne("DziennikSportowca.Models.Exercise", "Exercise")
                        .WithMany("TrainingPlans")
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DziennikSportowca.Models.TrainingPlan", "TrainingPlan")
                        .WithMany("Exercises")
                        .HasForeignKey("TrainingPlanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserFigure", b =>
                {
                    b.HasOne("DziennikSportowca.Models.ApplicationUser", "User")
                        .WithMany("UserCircumferences")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserFriend", b =>
                {
                    b.HasOne("DziennikSportowca.Models.ApplicationUser", "Friend")
                        .WithMany("Users")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DziennikSportowca.Models.ApplicationUser", "User")
                        .WithMany("UserFriends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserTraining", b =>
                {
                    b.HasOne("DziennikSportowca.Models.TrainingPlan", "Training")
                        .WithMany("UserTrainings")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DziennikSportowca.Models.UserTrainingExerciseResult", b =>
                {
                    b.HasOne("DziennikSportowca.Models.TrainingPlanExercise", "TrainingPlanExercise")
                        .WithMany("Results")
                        .HasForeignKey("TrainingPlanExerciseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DziennikSportowca.Models.UserTraining", "UserTraining")
                        .WithMany("UserTrainingsExercisesResults")
                        .HasForeignKey("UserTrainingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DziennikSportowca.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DziennikSportowca.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DziennikSportowca.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
