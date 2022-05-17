using Microsoft.AspNetCore.Components.Forms;

namespace SharpWeb_Server.Service.IService
{
	public interface IFileUpload
	{
		Task<string> UploadFile(IBrowserFile file);

		public bool DeleteFile(string filePath);
	}
}
