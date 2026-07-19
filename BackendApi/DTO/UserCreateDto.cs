namespace det_er_fredag.BackendApi.DTO;

public class UserCreateDto
{
	public required ulong ID { get; set; }

	public required string Username { get; set; }
	public required string DisplayName { get; set; }
}
