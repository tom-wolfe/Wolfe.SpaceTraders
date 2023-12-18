﻿namespace Wolfe.SpaceTraders.Infrastructure.Token
{
    internal class FileTokenService : ITokenReader, ITokenWriter
    {
        private readonly string _tempFile;
        private string? _token;

        public FileTokenService()
        {
            _tempFile = Path.Combine(Path.GetTempPath(), "spaceTraders.token");
        }

        public Task Write(string token, CancellationToken cancellationToken)
        {
            _token = token;
            return File.WriteAllTextAsync(_tempFile, token, cancellationToken);
        }

        public Task Clear(CancellationToken cancellationToken)
        {
            File.Delete(_tempFile);
            return Task.CompletedTask;
        }

        public async Task<string?> Read(CancellationToken cancellationToken)
        {
            return _token ??= await File.ReadAllTextAsync(_tempFile, cancellationToken);
        }
    }
}