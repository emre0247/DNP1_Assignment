using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository _postRepository;

    public ManagePostsView(IPostRepository postRepository)
    {
        this._postRepository = postRepository;
    }

    public async Task UpdatePostAsync(int postId, string newTitle, string newContent)
    {
        var post = await _postRepository.GetSingleAsync(postId);
        
        post.Title = newTitle;
        post.Body = newContent;
        
        await _postRepository.UpdateAsync(post);
        
        Console.WriteLine($"Updated post {post.Id}.");
    }
}