namespace Wolfe.SpaceTraders.Token
{
    internal class FileTokenService : ITokenGetService, ITokenSetService
    {
        private readonly string _tempFile;
        private string? _token;

        public FileTokenService()
        {
            _tempFile = Path.Combine(Path.GetTempPath(), "spaceTraders.token");
        }

        public Task SetToken(string token, CancellationToken cancellationToken)
        {
            _token = token;
            return File.WriteAllTextAsync(_tempFile, token, cancellationToken);
        }

        public async Task<string?> GetToken(CancellationToken cancellationToken)
        {
            return _token ??= await File.ReadAllTextAsync(_tempFile, cancellationToken);
        }
    }
}
