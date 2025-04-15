IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Classes] (
        [class_id] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        CONSTRAINT [PK_Classes] PRIMARY KEY ([class_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Users] (
        [user_id] int NOT NULL IDENTITY,
        [username] nvarchar(100) NOT NULL,
        [password_hash] nvarchar(max) NOT NULL,
        [email] nvarchar(max) NOT NULL,
        [role] nvarchar(20) NOT NULL,
        [ProfilePicturePath] nvarchar(max) NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([user_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Categories] (
        [category_id] int NOT NULL IDENTITY,
        [name] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [class_id] int NOT NULL,
        [uploaded_by] int NOT NULL,
        [user_id] int NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([category_id]),
        CONSTRAINT [FK_Categories_Classes_class_id] FOREIGN KEY ([class_id]) REFERENCES [Classes] ([class_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Categories_Users_uploaded_by] FOREIGN KEY ([uploaded_by]) REFERENCES [Users] ([user_id]),
        CONSTRAINT [FK_Categories_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Comics] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Comic_url] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [Uploaded_by] int NOT NULL,
        [Category_id] int NOT NULL,
        CONSTRAINT [PK_Comics] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Comics_Categories_Category_id] FOREIGN KEY ([Category_id]) REFERENCES [Categories] ([category_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Comics_Users_Uploaded_by] FOREIGN KEY ([Uploaded_by]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Documents] (
        [document_id] int NOT NULL IDENTITY,
        [title] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [file_path] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [category_id] int NOT NULL,
        [uploaded_by] int NOT NULL,
        CONSTRAINT [PK_Documents] PRIMARY KEY ([document_id]),
        CONSTRAINT [FK_Documents_Categories_category_id] FOREIGN KEY ([category_id]) REFERENCES [Categories] ([category_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Documents_Users_uploaded_by] FOREIGN KEY ([uploaded_by]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Exercises] (
        [exercise_id] int NOT NULL IDENTITY,
        [difficulty] nvarchar(20) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [title] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [category_id] int NOT NULL,
        [uploaded_by] int NOT NULL,
        CONSTRAINT [PK_Exercises] PRIMARY KEY ([exercise_id]),
        CONSTRAINT [FK_Exercises_Categories_category_id] FOREIGN KEY ([category_id]) REFERENCES [Categories] ([category_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Exercises_Users_uploaded_by] FOREIGN KEY ([uploaded_by]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Games] (
        [game_id] int NOT NULL IDENTITY,
        [game_url] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [classify] nvarchar(max) NOT NULL,
        [title] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [category_id] int NOT NULL,
        [uploaded_by] int NOT NULL,
        CONSTRAINT [PK_Games] PRIMARY KEY ([game_id]),
        CONSTRAINT [FK_Games_Categories_category_id] FOREIGN KEY ([category_id]) REFERENCES [Categories] ([category_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Games_Users_uploaded_by] FOREIGN KEY ([uploaded_by]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Lifes] (
        [Id] int NOT NULL IDENTITY,
        [Question] nvarchar(max) NOT NULL,
        [Answer] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [Uploaded_by] int NOT NULL,
        [Category_id] int NOT NULL,
        CONSTRAINT [PK_Lifes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Lifes_Categories_Category_id] FOREIGN KEY ([Category_id]) REFERENCES [Categories] ([category_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Lifes_Users_Uploaded_by] FOREIGN KEY ([Uploaded_by]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Videos] (
        [video_id] int NOT NULL IDENTITY,
        [video_url] nvarchar(max) NOT NULL,
        [description] nvarchar(max) NOT NULL,
        [title] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [category_id] int NOT NULL,
        [uploaded_by] int NOT NULL,
        CONSTRAINT [PK_Videos] PRIMARY KEY ([video_id]),
        CONSTRAINT [FK_Videos_Categories_category_id] FOREIGN KEY ([category_id]) REFERENCES [Categories] ([category_id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Videos_Users_uploaded_by] FOREIGN KEY ([uploaded_by]) REFERENCES [Users] ([user_id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Comments] (
        [comment_id] int NOT NULL IDENTITY,
        [content] nvarchar(max) NOT NULL,
        [created_at] datetime2 NOT NULL,
        [updated_at] datetime2 NOT NULL,
        [document_id] int NULL,
        [game_id] int NULL,
        [video_id] int NULL,
        [comic_id] int NULL,
        [user_id] int NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([comment_id]),
        CONSTRAINT [FK_Comments_Comics_comic_id] FOREIGN KEY ([comic_id]) REFERENCES [Comics] ([Id]),
        CONSTRAINT [FK_Comments_Documents_document_id] FOREIGN KEY ([document_id]) REFERENCES [Documents] ([document_id]),
        CONSTRAINT [FK_Comments_Games_game_id] FOREIGN KEY ([game_id]) REFERENCES [Games] ([game_id]),
        CONSTRAINT [FK_Comments_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]),
        CONSTRAINT [FK_Comments_Videos_video_id] FOREIGN KEY ([video_id]) REFERENCES [Videos] ([video_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE TABLE [Stars] (
        [star_id] int NOT NULL IDENTITY,
        [total_star] int NOT NULL,
        [document_id] int NULL,
        [exercise_id] int NULL,
        [game_id] int NULL,
        [video_id] int NULL,
        [comic_id] int NULL,
        [user_id] int NOT NULL,
        CONSTRAINT [PK_Stars] PRIMARY KEY ([star_id]),
        CONSTRAINT [FK_Stars_Comics_comic_id] FOREIGN KEY ([comic_id]) REFERENCES [Comics] ([Id]),
        CONSTRAINT [FK_Stars_Documents_document_id] FOREIGN KEY ([document_id]) REFERENCES [Documents] ([document_id]),
        CONSTRAINT [FK_Stars_Exercises_exercise_id] FOREIGN KEY ([exercise_id]) REFERENCES [Exercises] ([exercise_id]),
        CONSTRAINT [FK_Stars_Games_game_id] FOREIGN KEY ([game_id]) REFERENCES [Games] ([game_id]),
        CONSTRAINT [FK_Stars_Users_user_id] FOREIGN KEY ([user_id]) REFERENCES [Users] ([user_id]),
        CONSTRAINT [FK_Stars_Videos_video_id] FOREIGN KEY ([video_id]) REFERENCES [Videos] ([video_id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Categories_class_id] ON [Categories] ([class_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Categories_uploaded_by] ON [Categories] ([uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Categories_user_id] ON [Categories] ([user_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comics_Category_id] ON [Comics] ([Category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comics_Uploaded_by] ON [Comics] ([Uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comments_comic_id] ON [Comments] ([comic_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comments_document_id] ON [Comments] ([document_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comments_game_id] ON [Comments] ([game_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comments_user_id] ON [Comments] ([user_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Comments_video_id] ON [Comments] ([video_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Documents_category_id] ON [Documents] ([category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Documents_uploaded_by] ON [Documents] ([uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Exercises_category_id] ON [Exercises] ([category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Exercises_uploaded_by] ON [Exercises] ([uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Games_category_id] ON [Games] ([category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Games_uploaded_by] ON [Games] ([uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Lifes_Category_id] ON [Lifes] ([Category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Lifes_Uploaded_by] ON [Lifes] ([Uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Stars_comic_id] ON [Stars] ([comic_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Stars_document_id] ON [Stars] ([document_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Stars_exercise_id] ON [Stars] ([exercise_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Stars_game_id] ON [Stars] ([game_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Stars_user_id_comic_id] ON [Stars] ([user_id], [comic_id]) WHERE [comic_id] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Stars_user_id_document_id] ON [Stars] ([user_id], [document_id]) WHERE [document_id] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Stars_user_id_exercise_id] ON [Stars] ([user_id], [exercise_id]) WHERE [exercise_id] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Stars_user_id_game_id] ON [Stars] ([user_id], [game_id]) WHERE [game_id] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_Stars_user_id_video_id] ON [Stars] ([user_id], [video_id]) WHERE [video_id] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Stars_video_id] ON [Stars] ([video_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Videos_category_id] ON [Videos] ([category_id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    CREATE INDEX [IX_Videos_uploaded_by] ON [Videos] ([uploaded_by]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411023942_updateForeignKey'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250411023942_updateForeignKey', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411043631_addContentTypeStar'
)
BEGIN
    ALTER TABLE [Stars] ADD [ContentType] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250411043631_addContentTypeStar'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250411043631_addContentTypeStar', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Videos] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Users] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Lifes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Games] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Exercises] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Documents] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Comics] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Classes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    ALTER TABLE [Categories] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250413124939_AddIsDeleted'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250413124939_AddIsDeleted', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250414044351_AddIsDeletedToLife'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250414044351_AddIsDeletedToLife', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415161813_questionSet'
)
BEGIN
    ALTER TABLE [Lifes] ADD [QuestionSet] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415161813_questionSet'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250415161813_questionSet', N'9.0.3');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415173258_AddQuestionSetAndCategoryToComment'
)
BEGIN
    ALTER TABLE [Comments] ADD [category_id] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415173258_AddQuestionSetAndCategoryToComment'
)
BEGIN
    ALTER TABLE [Comments] ADD [question_set] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250415173258_AddQuestionSetAndCategoryToComment'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250415173258_AddQuestionSetAndCategoryToComment', N'9.0.3');
END;

COMMIT;
GO

