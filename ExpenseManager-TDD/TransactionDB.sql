USE [TransactionDB]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [Title], [Icon], [Type]) VALUES (10, N'Travel', N'✈️', N'Expense')
INSERT [dbo].[Categories] ([CategoryID], [Title], [Icon], [Type]) VALUES (11, N'Car', N'🚗', N'Expense')
INSERT [dbo].[Categories] ([CategoryID], [Title], [Icon], [Type]) VALUES (12, N'Salary', N'💵', N'Income')
INSERT [dbo].[Categories] ([CategoryID], [Title], [Icon], [Type]) VALUES (17, N'Food', N'🍟', N'Expense')
INSERT [dbo].[Categories] ([CategoryID], [Title], [Icon], [Type]) VALUES (18, N'Gift', N'🎁', N'Expense')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([TransactionID], [CategoryID], [Amount], [Note], [Date]) VALUES (1, 17, 75, N'Dominoese', CAST(N'2023-12-08T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Transactions] ([TransactionID], [CategoryID], [Amount], [Note], [Date]) VALUES (4, 12, 100, N'Job', CAST(N'2023-12-08T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Transactions] ([TransactionID], [CategoryID], [Amount], [Note], [Date]) VALUES (14, 18, 10, N'Clean', CAST(N'2023-12-09T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Transactions] ([TransactionID], [CategoryID], [Amount], [Note], [Date]) VALUES (15, 12, 55, N'22', CAST(N'2023-12-09T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Transactions] ([TransactionID], [CategoryID], [Amount], [Note], [Date]) VALUES (17, 11, 3, N'csc', CAST(N'2023-12-09T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231130203928_Initial Create', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231209001938_Updated Models', N'7.0.14')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231215024025_Last Migration', N'7.0.14')
GO
