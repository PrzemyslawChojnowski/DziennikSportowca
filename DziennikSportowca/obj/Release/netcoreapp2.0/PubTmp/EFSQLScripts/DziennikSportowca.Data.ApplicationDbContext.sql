IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [Email] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [LockoutEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [PasswordHash] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [UserName] nvarchar(256) NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'00000000000000_CreateIdentitySchema', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    DROP INDEX [IX_AspNetUserRoles_UserId] ON [AspNetUserRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    DROP INDEX [RoleNameIndex] ON [AspNetRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    CREATE TABLE [Exercises] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Exercises] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    CREATE TABLE [TrainingPlans] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_TrainingPlans] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_TrainingPlans_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    CREATE TABLE [TrainingPlanExercises] (
        [ExerciseId] int NOT NULL,
        [TrainingPlanId] int NOT NULL,
        CONSTRAINT [PK_TrainingPlanExercises] PRIMARY KEY ([ExerciseId], [TrainingPlanId]),
        CONSTRAINT [FK_TrainingPlanExercises_Exercises_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercises] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TrainingPlanExercises_TrainingPlans_TrainingPlanId] FOREIGN KEY ([TrainingPlanId]) REFERENCES [TrainingPlans] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    CREATE INDEX [IX_TrainingPlans_UserId] ON [TrainingPlans] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    CREATE INDEX [IX_TrainingPlanExercises_TrainingPlanId] ON [TrainingPlanExercises] ([TrainingPlanId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523173845_Workout_Plans_And_Exercises_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170523173845_Workout_Plans_And_Exercises_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlans] DROP CONSTRAINT [FK_TrainingPlans_AspNetUsers_UserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercises] DROP CONSTRAINT [FK_TrainingPlanExercises_Exercises_ExerciseId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercises] DROP CONSTRAINT [FK_TrainingPlanExercises_TrainingPlans_TrainingPlanId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercises] DROP CONSTRAINT [PK_TrainingPlanExercises];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlans] DROP CONSTRAINT [PK_TrainingPlans];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [Exercises] DROP CONSTRAINT [PK_Exercises];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    EXEC sp_rename N'TrainingPlanExercises', N'TrainingPlanExercise';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    EXEC sp_rename N'TrainingPlans', N'TrainingPlan';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    EXEC sp_rename N'Exercises', N'Exercise';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    EXEC sp_rename N'TrainingPlanExercise.IX_TrainingPlanExercises_TrainingPlanId', N'IX_TrainingPlanExercise_TrainingPlanId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    EXEC sp_rename N'TrainingPlan.IX_TrainingPlans_UserId', N'IX_TrainingPlan_UserId', N'INDEX';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD CONSTRAINT [PK_TrainingPlanExercise] PRIMARY KEY ([ExerciseId], [TrainingPlanId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlan] ADD CONSTRAINT [PK_TrainingPlan] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [Exercise] ADD CONSTRAINT [PK_Exercise] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    CREATE TABLE [MusclePart] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_MusclePart] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    CREATE TABLE [MusclePartExercise] (
        [ExerciseId] int NOT NULL,
        [MuscePartId] int NOT NULL,
        CONSTRAINT [PK_MusclePartExercise] PRIMARY KEY ([ExerciseId], [MuscePartId]),
        CONSTRAINT [FK_MusclePartExercise_Exercise_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_MusclePartExercise_MusclePart_MuscePartId] FOREIGN KEY ([MuscePartId]) REFERENCES [MusclePart] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    CREATE INDEX [IX_MusclePartExercise_MuscePartId] ON [MusclePartExercise] ([MuscePartId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlan] ADD CONSTRAINT [FK_TrainingPlan_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD CONSTRAINT [FK_TrainingPlanExercise_Exercise_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD CONSTRAINT [FK_TrainingPlanExercise_TrainingPlan_TrainingPlanId] FOREIGN KEY ([TrainingPlanId]) REFERENCES [TrainingPlan] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170523204008_MuscleParts_And_Exercises_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170523204008_MuscleParts_And_Exercises_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170717120416_TrainingPlanExercise_Updated')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD [RepsNo] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170717120416_TrainingPlanExercise_Updated')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD [SeriesNo] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170717120416_TrainingPlanExercise_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170717120416_TrainingPlanExercise_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170719145400_TrainingPlanExercises_Entity_Edited')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD [Id] int NOT NULL IDENTITY;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170719145400_TrainingPlanExercises_Entity_Edited')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD CONSTRAINT [AK_TrainingPlanExercise_Id] UNIQUE ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170719145400_TrainingPlanExercises_Entity_Edited')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170719145400_TrainingPlanExercises_Entity_Edited', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] DROP CONSTRAINT [AK_TrainingPlanExercise_Id];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] DROP CONSTRAINT [PK_TrainingPlanExercise];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Name] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Surname] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD CONSTRAINT [PK_TrainingPlanExercise] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    CREATE INDEX [IX_TrainingPlanExercise_ExerciseId] ON [TrainingPlanExercise] ([ExerciseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170720181100_ApplicationUser_Entity_Edited_Name_And_Surname_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723142613_FoodProduct_Entity_Added')
BEGIN
    CREATE TABLE [FoodProduct] (
        [Id] int NOT NULL IDENTITY,
        [Carbohydrate] float NOT NULL,
        [Description] nvarchar(max) NULL,
        [Energy] float NOT NULL,
        [Fat] float NOT NULL,
        [Measurement] int NOT NULL,
        [Protein] float NOT NULL,
        CONSTRAINT [PK_FoodProduct] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723142613_FoodProduct_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170723142613_FoodProduct_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723145446_DishFoodProduct_And_Dish_Entities_Added')
BEGIN
    CREATE TABLE [Dish] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Dish] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723145446_DishFoodProduct_And_Dish_Entities_Added')
BEGIN
    CREATE TABLE [DishFoodProduct] (
        [DishId] int NOT NULL,
        [FoodProductId] int NOT NULL,
        CONSTRAINT [PK_DishFoodProduct] PRIMARY KEY ([DishId], [FoodProductId]),
        CONSTRAINT [FK_DishFoodProduct_Dish_DishId] FOREIGN KEY ([DishId]) REFERENCES [Dish] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_DishFoodProduct_FoodProduct_FoodProductId] FOREIGN KEY ([FoodProductId]) REFERENCES [FoodProduct] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723145446_DishFoodProduct_And_Dish_Entities_Added')
BEGIN
    CREATE INDEX [IX_DishFoodProduct_FoodProductId] ON [DishFoodProduct] ([FoodProductId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723145446_DishFoodProduct_And_Dish_Entities_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170723145446_DishFoodProduct_And_Dish_Entities_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723150324_ApplicationUser_Entity_Updated')
BEGIN
    ALTER TABLE [Dish] ADD [UserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723150324_ApplicationUser_Entity_Updated')
BEGIN
    CREATE INDEX [IX_Dish_UserId] ON [Dish] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723150324_ApplicationUser_Entity_Updated')
BEGIN
    ALTER TABLE [Dish] ADD CONSTRAINT [FK_Dish_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170723150324_ApplicationUser_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170723150324_ApplicationUser_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170724131130_UserFigure_Entity_Added')
BEGIN
    CREATE TABLE [UserFigure] (
        [Id] int NOT NULL IDENTITY,
        [BicepsCircumference] float NOT NULL,
        [BodyFat] float NOT NULL,
        [ChestCircumference] float NOT NULL,
        [Date] datetime2 NOT NULL,
        [HipCircumference] float NOT NULL,
        [ShouldersCircumference] float NOT NULL,
        [ThighCircumference] float NOT NULL,
        [TricepsCircumference] float NOT NULL,
        [UserId] nvarchar(450) NULL,
        [WaistCircumference] float NOT NULL,
        [Weight] float NOT NULL,
        CONSTRAINT [PK_UserFigure] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserFigure_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170724131130_UserFigure_Entity_Added')
BEGIN
    CREATE INDEX [IX_UserFigure_UserId] ON [UserFigure] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170724131130_UserFigure_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170724131130_UserFigure_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170725140203_ApplicationUser_Entity_Update')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [Gender] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170725140203_ApplicationUser_Entity_Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170725140203_ApplicationUser_Entity_Update', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170726161109_SomeChangesInEntitiesMade')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170726161109_SomeChangesInEntitiesMade', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170729160439_ApplicationUser_Entity_Edited_UserProfilePicture_Added')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ProfilePicture] varbinary(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170729160439_ApplicationUser_Entity_Edited_UserProfilePicture_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170729160439_ApplicationUser_Entity_Edited_UserProfilePicture_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170731132416_Entity_Photo_Added')
BEGIN
    CREATE TABLE [Photo] (
        [Id] int NOT NULL IDENTITY,
        [PhotoContent] varbinary(max) NULL,
        [UserFigureId] int NOT NULL,
        CONSTRAINT [PK_Photo] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Photo_UserFigure_UserFigureId] FOREIGN KEY ([UserFigureId]) REFERENCES [UserFigure] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170731132416_Entity_Photo_Added')
BEGIN
    CREATE INDEX [IX_Photo_UserFigureId] ON [Photo] ([UserFigureId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170731132416_Entity_Photo_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170731132416_Entity_Photo_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170923144312_ApplicationUser_Entity_Updated_2')
BEGIN
    ALTER TABLE [AspNetUsers] ADD [ApplicationUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170923144312_ApplicationUser_Entity_Updated_2')
BEGIN
    CREATE INDEX [IX_AspNetUsers_ApplicationUserId] ON [AspNetUsers] ([ApplicationUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170923144312_ApplicationUser_Entity_Updated_2')
BEGIN
    ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170923144312_ApplicationUser_Entity_Updated_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170923144312_ApplicationUser_Entity_Updated_2', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924081718_UserFriend_Entity_Added')
BEGIN
    ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_AspNetUsers_ApplicationUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924081718_UserFriend_Entity_Added')
BEGIN
    DROP INDEX [IX_AspNetUsers_ApplicationUserId] ON [AspNetUsers];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924081718_UserFriend_Entity_Added')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'AspNetUsers') AND [c].[name] = N'ApplicationUserId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AspNetUsers] DROP COLUMN [ApplicationUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924081718_UserFriend_Entity_Added')
BEGIN
    CREATE TABLE [UserFriend] (
        [UserId] nvarchar(450) NOT NULL,
        [FriendId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_UserFriend] PRIMARY KEY ([UserId], [FriendId]),
        CONSTRAINT [FK_UserFriend_AspNetUsers_FriendId] FOREIGN KEY ([FriendId]) REFERENCES [AspNetUsers] ([Id]),
        CONSTRAINT [FK_UserFriend_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924081718_UserFriend_Entity_Added')
BEGIN
    CREATE INDEX [IX_UserFriend_FriendId] ON [UserFriend] ([FriendId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924081718_UserFriend_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170924081718_UserFriend_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924133556_UserFriend_Entity_Updated_1')
BEGIN
    ALTER TABLE [UserFriend] ADD [FriendshipStatus] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170924133556_UserFriend_Entity_Updated_1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170924133556_UserFriend_Entity_Updated_1', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170929154310_UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made')
BEGIN
    CREATE TABLE [UserTraining] (
        [Id] int NOT NULL IDENTITY,
        [TrainingDate] datetime2 NOT NULL,
        [TrainingId] int NOT NULL,
        CONSTRAINT [PK_UserTraining] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_UserTraining_TrainingPlan_TrainingId] FOREIGN KEY ([TrainingId]) REFERENCES [TrainingPlan] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170929154310_UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made')
BEGIN
    CREATE TABLE [UserTrainingExerciseResult] (
        [UserTrainingId] int NOT NULL,
        [TrainingPlanExerciseId] int NOT NULL,
        [RepsNo] int NOT NULL,
        [SeriesNo] int NOT NULL,
        CONSTRAINT [PK_UserTrainingExerciseResult] PRIMARY KEY ([UserTrainingId], [TrainingPlanExerciseId]),
        CONSTRAINT [FK_UserTrainingExerciseResult_TrainingPlanExercise_TrainingPlanExerciseId] FOREIGN KEY ([TrainingPlanExerciseId]) REFERENCES [TrainingPlanExercise] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_UserTrainingExerciseResult_UserTraining_UserTrainingId] FOREIGN KEY ([UserTrainingId]) REFERENCES [UserTraining] ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170929154310_UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made')
BEGIN
    CREATE INDEX [IX_UserTraining_TrainingId] ON [UserTraining] ([TrainingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170929154310_UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made')
BEGIN
    CREATE INDEX [IX_UserTrainingExerciseResult_TrainingPlanExerciseId] ON [UserTrainingExerciseResult] ([TrainingPlanExerciseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20170929154310_UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20170929154310_UserTraining_And_UserTrainingExercisesResults_Added_And_Some_Changes_Made', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    ALTER TABLE [UserTrainingExerciseResult] DROP CONSTRAINT [PK_UserTrainingExerciseResult];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    EXEC sp_rename N'UserTraining.TrainingDate', N'StartDateTime', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    ALTER TABLE [UserTrainingExerciseResult] ADD [Id] int NOT NULL IDENTITY;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    ALTER TABLE [UserTraining] ADD [EndDateTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    ALTER TABLE [UserTrainingExerciseResult] ADD CONSTRAINT [PK_UserTrainingExerciseResult] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    CREATE TABLE [ExerciseWeight] (
        [Id] int NOT NULL IDENTITY,
        [UserTrainingExerciseResultId] int NOT NULL,
        [Weight] float NOT NULL,
        CONSTRAINT [PK_ExerciseWeight] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ExerciseWeight_UserTrainingExerciseResult_UserTrainingExerciseResultId] FOREIGN KEY ([UserTrainingExerciseResultId]) REFERENCES [UserTrainingExerciseResult] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    CREATE INDEX [IX_UserTrainingExerciseResult_UserTrainingId] ON [UserTrainingExerciseResult] ([UserTrainingId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    CREATE INDEX [IX_ExerciseWeight_UserTrainingExerciseResultId] ON [ExerciseWeight] ([UserTrainingExerciseResultId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171005113058_ExerciseWeight_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171005113058_ExerciseWeight_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    DROP INDEX [UserNameIndex] ON [AspNetUsers];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    DROP INDEX [RoleNameIndex] ON [AspNetRoles];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD [Index] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    ALTER TABLE [AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171012140600_TrainingPlanExercises_Entity_Index_Property_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171016194948_TrainingPlan_Entity_CreationDate_Property_Added')
BEGIN
    ALTER TABLE [TrainingPlan] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171016194948_TrainingPlan_Entity_CreationDate_Property_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171016194948_TrainingPlan_Entity_CreationDate_Property_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171018065828_FoodProductType_Entity_Added')
BEGIN
    ALTER TABLE [FoodProduct] ADD [TypeId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171018065828_FoodProductType_Entity_Added')
BEGIN
    CREATE TABLE [FoodProductType] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_FoodProductType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171018065828_FoodProductType_Entity_Added')
BEGIN
    CREATE INDEX [IX_FoodProduct_TypeId] ON [FoodProduct] ([TypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171018065828_FoodProductType_Entity_Added')
BEGIN
    ALTER TABLE [FoodProduct] ADD CONSTRAINT [FK_FoodProduct_FoodProductType_TypeId] FOREIGN KEY ([TypeId]) REFERENCES [FoodProductType] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171018065828_FoodProductType_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171018065828_FoodProductType_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171025191227_DishFoodProduct_Entity_Updated')
BEGIN
    ALTER TABLE [DishFoodProduct] ADD [FoodProductWeight] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171025191227_DishFoodProduct_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171025191227_DishFoodProduct_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171029160253_ActvityType_Entity_Added')
BEGIN
    ALTER TABLE [Exercise] ADD [ActivityTypeId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171029160253_ActvityType_Entity_Added')
BEGIN
    CREATE TABLE [ActivityType] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_ActivityType] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171029160253_ActvityType_Entity_Added')
BEGIN
    CREATE INDEX [IX_Exercise_ActivityTypeId] ON [Exercise] ([ActivityTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171029160253_ActvityType_Entity_Added')
BEGIN
    ALTER TABLE [Exercise] ADD CONSTRAINT [FK_Exercise_ActivityType_ActivityTypeId] FOREIGN KEY ([ActivityTypeId]) REFERENCES [ActivityType] ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171029160253_ActvityType_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171029160253_ActvityType_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    ALTER TABLE [Exercise] ADD [ExerciseInstructionId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    CREATE TABLE [ExerciseInstruction] (
        [Id] int NOT NULL IDENTITY,
        [ExerciseId] int NOT NULL,
        [Instructions] nvarchar(max) NULL,
        CONSTRAINT [PK_ExerciseInstruction] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ExerciseInstruction_Exercise_ExerciseId] FOREIGN KEY ([ExerciseId]) REFERENCES [Exercise] ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    CREATE TABLE [ExerciseInstructionPhoto] (
        [Id] int NOT NULL IDENTITY,
        [Content] varbinary(max) NULL,
        [ExerciseInstructionId] int NOT NULL,
        [PhotoTitle] nvarchar(max) NULL,
        CONSTRAINT [PK_ExerciseInstructionPhoto] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId] FOREIGN KEY ([ExerciseInstructionId]) REFERENCES [ExerciseInstruction] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    CREATE INDEX [IX_Exercise_ExerciseInstructionId] ON [Exercise] ([ExerciseInstructionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    CREATE INDEX [IX_ExerciseInstruction_ExerciseId] ON [ExerciseInstruction] ([ExerciseId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    CREATE INDEX [IX_ExerciseInstructionPhoto_ExerciseInstructionId] ON [ExerciseInstructionPhoto] ([ExerciseInstructionId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    ALTER TABLE [Exercise] ADD CONSTRAINT [FK_Exercise_ExerciseInstruction_ExerciseInstructionId] FOREIGN KEY ([ExerciseInstructionId]) REFERENCES [ExerciseInstruction] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171102135854_ExerciseInstruction_And_ExerciseInstructionPhoto_Entities_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102153526_Exercise_Entity_Updated')
BEGIN
    ALTER TABLE [Exercise] DROP CONSTRAINT [FK_Exercise_ExerciseInstruction_ExerciseInstructionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102153526_Exercise_Entity_Updated')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Exercise') AND [c].[name] = N'ExerciseInstructionId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Exercise] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Exercise] ALTER COLUMN [ExerciseInstructionId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102153526_Exercise_Entity_Updated')
BEGIN
    ALTER TABLE [Exercise] ADD CONSTRAINT [FK_Exercise_ExerciseInstruction_ExerciseInstructionId] FOREIGN KEY ([ExerciseInstructionId]) REFERENCES [ExerciseInstruction] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102153526_Exercise_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171102153526_Exercise_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102161137_ExerciseInstructionPhoto_Entity_Updated')
BEGIN
    ALTER TABLE [ExerciseInstructionPhoto] DROP CONSTRAINT [FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102161137_ExerciseInstructionPhoto_Entity_Updated')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'ExerciseInstructionPhoto') AND [c].[name] = N'ExerciseInstructionId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ExerciseInstructionPhoto] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [ExerciseInstructionPhoto] ALTER COLUMN [ExerciseInstructionId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102161137_ExerciseInstructionPhoto_Entity_Updated')
BEGIN
    ALTER TABLE [ExerciseInstructionPhoto] ADD CONSTRAINT [FK_ExerciseInstructionPhoto_ExerciseInstruction_ExerciseInstructionId] FOREIGN KEY ([ExerciseInstructionId]) REFERENCES [ExerciseInstruction] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171102161137_ExerciseInstructionPhoto_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171102161137_ExerciseInstructionPhoto_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171106223345_Goal_Entity_Added')
BEGIN
    CREATE TABLE [Goal] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(max) NULL,
        [Result] bit NOT NULL,
        [Scopes] nvarchar(max) NULL,
        [Target] nvarchar(max) NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Goal] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Goal_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171106223345_Goal_Entity_Added')
BEGIN
    CREATE INDEX [IX_Goal_UserId] ON [Goal] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171106223345_Goal_Entity_Added')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171106223345_Goal_Entity_Added', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171107205355_Goal_Entity_Updated_1')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Goal') AND [c].[name] = N'Scopes');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Goal] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Goal] DROP COLUMN [Scopes];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171107205355_Goal_Entity_Updated_1')
BEGIN
    ALTER TABLE [Goal] ADD [Scope] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171107205355_Goal_Entity_Updated_1')
BEGIN
    ALTER TABLE [Goal] ADD [ScopePosition] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171107205355_Goal_Entity_Updated_1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171107205355_Goal_Entity_Updated_1', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171111193528_Goal_Entity_Updated_2')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Goal') AND [c].[name] = N'ScopePosition');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Goal] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Goal] DROP COLUMN [ScopePosition];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171111193528_Goal_Entity_Updated_2')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Goal') AND [c].[name] = N'Target');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Goal] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Goal] ALTER COLUMN [Target] float NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171111193528_Goal_Entity_Updated_2')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'Goal') AND [c].[name] = N'Scope');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Goal] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Goal] ALTER COLUMN [Scope] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171111193528_Goal_Entity_Updated_2')
BEGIN
    ALTER TABLE [Goal] ADD [CompletionDate] datetime2 NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171111193528_Goal_Entity_Updated_2')
BEGIN
    ALTER TABLE [Goal] ADD [CreationDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171111193528_Goal_Entity_Updated_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171111193528_Goal_Entity_Updated_2', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171113205846_TrainingPlanExercise_Entity_Updated')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'TrainingPlanExercise') AND [c].[name] = N'SeriesNo');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [TrainingPlanExercise] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [TrainingPlanExercise] ALTER COLUMN [SeriesNo] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171113205846_TrainingPlanExercise_Entity_Updated')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'TrainingPlanExercise') AND [c].[name] = N'RepsNo');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [TrainingPlanExercise] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [TrainingPlanExercise] ALTER COLUMN [RepsNo] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171113205846_TrainingPlanExercise_Entity_Updated')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD [ExerciseLength] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171113205846_TrainingPlanExercise_Entity_Updated')
BEGIN
    ALTER TABLE [TrainingPlanExercise] ADD [TrainingPlanExerciseInfo] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171113205846_TrainingPlanExercise_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171113205846_TrainingPlanExercise_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115153347_UserTrainingExerciseResult_Entity_Updated')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'UserTrainingExerciseResult') AND [c].[name] = N'SeriesNo');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [UserTrainingExerciseResult] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [UserTrainingExerciseResult] ALTER COLUMN [SeriesNo] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115153347_UserTrainingExerciseResult_Entity_Updated')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'UserTrainingExerciseResult') AND [c].[name] = N'RepsNo');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [UserTrainingExerciseResult] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [UserTrainingExerciseResult] ALTER COLUMN [RepsNo] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115153347_UserTrainingExerciseResult_Entity_Updated')
BEGIN
    ALTER TABLE [UserTrainingExerciseResult] ADD [ExerciseLength] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115153347_UserTrainingExerciseResult_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171115153347_UserTrainingExerciseResult_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115181458_ExerciseWeight_Entity_Updated')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'TrainingPlanExercise') AND [c].[name] = N'TrainingPlanExerciseInfo');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [TrainingPlanExercise] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [TrainingPlanExercise] DROP COLUMN [TrainingPlanExerciseInfo];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115181458_ExerciseWeight_Entity_Updated')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'ExerciseWeight') AND [c].[name] = N'Weight');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ExerciseWeight] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [ExerciseWeight] DROP COLUMN [Weight];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115181458_ExerciseWeight_Entity_Updated')
BEGIN
    ALTER TABLE [ExerciseWeight] ADD [Result] float NOT NULL DEFAULT 0E0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171115181458_ExerciseWeight_Entity_Updated')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171115181458_ExerciseWeight_Entity_Updated', N'2.0.0-rtm-26452');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171125113232_DishFoodProduct_Entity_Update')
BEGIN
    ALTER TABLE [DishFoodProduct] DROP CONSTRAINT [PK_DishFoodProduct];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171125113232_DishFoodProduct_Entity_Update')
BEGIN
    ALTER TABLE [DishFoodProduct] ADD [Id] int NOT NULL IDENTITY;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171125113232_DishFoodProduct_Entity_Update')
BEGIN
    ALTER TABLE [DishFoodProduct] ADD CONSTRAINT [PK_DishFoodProduct] PRIMARY KEY ([Id]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171125113232_DishFoodProduct_Entity_Update')
BEGIN
    CREATE INDEX [IX_DishFoodProduct_DishId] ON [DishFoodProduct] ([DishId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20171125113232_DishFoodProduct_Entity_Update')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20171125113232_DishFoodProduct_Entity_Update', N'2.0.0-rtm-26452');
END;

GO

