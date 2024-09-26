// See https://aka.ms/new-console-template for more information

using CLI.UI;
using FileRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app... ");
IUserRepository userRepository = new UserFileRepository(); // Old: new UserInMemoryRepository();
ICommentRepository commentRepository = new CommentFileRepository(); // Old: new CommentInMemoryRepository();
IPostRepository postRepository = new PostFileRepository(); // Old: new PostInMemoryRepository();

CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);

await cliApp.StartAsync();