using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public async Task<Post> GetSinglePost(int postId)
    {
        Post? postToGet = await postRepository.GetSingleAsync(postId);
        if (postToGet is null)
        {
            Console.Error.WriteLine($"No post found with id {postId}");
        }
        Console.WriteLine($"Post Body: {postToGet.Body} - Post Id: {postToGet.Id} - Post Title: {postToGet.Title}");
        return postToGet;
    }
}