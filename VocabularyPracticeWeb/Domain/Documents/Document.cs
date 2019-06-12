using System;

namespace VocabularyPracticeWeb.Domain.Documents
{
	public class Document
	{
		public Document()
		{
		}

		public Document(byte[] data, string fileName, DocumentType type)
		{
			EncodedContent = Encode(data);
			Type = type;
			FileName = fileName;
		}

		public string Id { get; set; }

		public string RevId { get; set; }

		public string EncodedContent { get; }

		public string FileName { get; set; }

		public DocumentType Type { get; }

		public byte[] Content { get { return Decode(EncodedContent); } }

		private string Encode(byte[] data)
		{
			return data != null ? Convert.ToBase64String(data) : null;
		}

		private byte[] Decode(string encoded)
		{
			return encoded != null ? Convert.FromBase64String(encoded) : null;
		}
	}
}
