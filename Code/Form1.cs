using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;

//using System.Text.Enc
namespace TRITPO_PROJECT
{
	public partial class Form1 : Form
	{
		//System.Text.Encoding.
		//setlocale(LC_ALL, "Russian");
		string[] buf_path;
		string[] path = new string[3];
		string file_content;
		string filename_extension = "";
		string file_name = "";
		string new_file_path = "";
		string endecrypted_file_content = "";

		public Form1()
		{
			InitializeComponent();
			textBox3.Text = "";
			panel1.BackColor = Color.LightBlue;
			comboBox1.SelectedIndex = 0;
			//checkedListBox1.set
			//checkedListBox1.SetItemChecked(0, true);
			//checkedListBox1.SetItemCheckState(0,CheckState.Checked);
		}

		private void Label1_Click(object sender, EventArgs e)
		{

		}

		private void TextBox3_TextChanged(object sender, EventArgs e)
		{

		}

		private void Label4_Click(object sender, EventArgs e)
		{

		}

		private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void Label5_Click(object sender, EventArgs e)
		{

		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void Label2_Click(object sender, EventArgs e)
		{

		}

		private void Panel1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.All;
			panel1.BackColor = Color.LightGreen;
		}

		private void Label4_Click_1(object sender, EventArgs e)
		{
			Console.WriteLine("Please enter a password to use:");
			string password = Console.ReadLine();
			Console.WriteLine("Please enter a string to encrypt:");
			string plaintext = Console.ReadLine();
			Console.WriteLine("");

			Console.WriteLine("Your encrypted string is:");
			string encryptedstring = StringCipher.EncryptAES(plaintext, password);
			Console.WriteLine(encryptedstring);
			Console.WriteLine("");

			string password2 = Console.ReadLine();
			Console.WriteLine("Your decrypted string is:");
			string decryptedstring = "";
			try { decryptedstring = StringCipher.DecryptAES(encryptedstring, password2); }
			catch {
				Console.WriteLine("debil?");
			}
			Console.WriteLine(decryptedstring);
			Console.WriteLine("");

			Console.WriteLine("Press any key to exit...");
			Console.ReadLine();
		}

		private void Panel1_DragDrop(object sender, DragEventArgs e)
		{
			ChangeStatus("", Color.Green);
			file_name = "";
			filename_extension = "";
			buf_path = (string[])e.Data.GetData(DataFormats.FileDrop);
			path[0] = buf_path[0];
			path[1] = path[0];
			while (path[1][path[1].Length - 1] != '\\')
			{
				
				file_name = file_name.Insert(0, path[1][(path[1].Length - 1)].ToString());
				path[1] = path[1].Remove(path[1].Length - 1);
			}

			int j = 0;
			while (file_name[file_name.Length - 1 - j] != '.')
			{
				if (file_name.Length - 1 - j == 0) { filename_extension = ""; break; }
				filename_extension = filename_extension.Insert(0, file_name[file_name.Length - 1 - j].ToString());
				j++;
			}
			Console.WriteLine(file_name);
			Console.WriteLine(filename_extension);
			//file_name = file_name
			path[2] = path[1];
			path[1] += "Encrypted"; //+ "." + filename_extension;
			//path[2] += "Decrypted" + "." + filename_extension;
			panel1.BackColor = Color.LightBlue;
			label2.Text = file_name.Insert(0, "                  "); 
			//label6.Text = path[0];
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			if (file_name == "")
			{
				ChangeStatus("Выберите файл!", Color.Red);
				return;
			}
			if (!((comboBox1.SelectedIndex >= 0) && (comboBox1.SelectedIndex <= 1)))
			{
				ChangeStatus("Выберите шифр!", Color.Red);
				return;
			}
			if (!((checkedListBox1.SelectedIndex >= 0) && (checkedListBox1.SelectedIndex <= 1))) {
				ChangeStatus("Выберите режим!", Color.Red);
				return;
			}		

			if (textBox3.Text == "")
			{
				textBox3.Text = StringCipher.GenerateKey();
				//textBox3.Text = StringCipher.Generate256BitsOfRandomEntropy().ToString();
				//ChangeStatus("Введите ключ!", Color.Red);
				//return;
			}

			//ChangeStatus("Обработка...", Color.LightYellow);
			ChangeStatus("Обработка...", Color.Olive);
			using (StreamReader streamReader = new StreamReader(path[0], Encoding.Default))
			{
				file_content = streamReader.ReadToEnd();
			}

			
			int EnDecrypting = checkedListBox1.SelectedIndex;
			if (EnDecrypting == 0) PerformEncryption(textBox3.Text, comboBox1.SelectedItem.ToString(), file_name);
			else PerformDecryption(textBox3.Text, comboBox1.SelectedItem.ToString(), file_name);

			if (label4.Text != "Неверный ключ!")
			ChangeStatus("Готово!", Color.Green);
			label2.Text = "Переместите файл в поле..." ;
			//Console.WriteLine(file_content);
			//Console.WriteLine(path[0]);
			//Console.WriteLine(path[1]);
			//Console.WriteLine(path[2]);



			//if (checkedListBox1.SelectedIndex == 0) endecrypted_file_content = StringCipher.Encrypt(file_content, textBox3.Text);
			//else endecrypted_file_content = StringCipher.Decrypt(file_content, textBox3.Text);
			//File.WriteAllText(new_file_path, endecrypted_file_content, Encoding.Default);
		}

		private void PerformEncryption(string Key, string Method, string file_name)
		{
			
			new_file_path = path[1];

			
			endecrypted_file_content = "";

			switch (Method)
			{
				case "AES":
					{

						 endecrypted_file_content = StringCipher.EncryptAES(file_content + "$$$" + file_name, textBox3.Text); 
						
						
					}
					break;
				case "DES":
					{
						endecrypted_file_content = StringCipher.EncryptDES(file_content + "$$$" + file_name, textBox3.Text);

					}
					break;
			}

			FileStream fs_create = File.Create(new_file_path);
			fs_create.Close();
			//endecrypted_file_content += filename_extension; 
			File.WriteAllText(new_file_path, endecrypted_file_content, Encoding.Default);
			return;
		}

		private void PerformDecryption(string Key, string Method, string file_name)
		{

			new_file_path = path[2];

			endecrypted_file_content = "";

			switch (Method)
			{
				case "AES":
					{

						try
						{
							endecrypted_file_content = StringCipher.DecryptAES(file_content, textBox3.Text);
						}
						catch { ChangeStatus("Неверный ключ!", Color.Red); return; }

					}
					break;
				case "DES":
					{
						endecrypted_file_content = StringCipher.DecryptDES(file_content, textBox3.Text);

					}
					break;


			}

			int j = 0;
			filename_extension = "";
			while (endecrypted_file_content[endecrypted_file_content.Length - 1 - j] != '$' &&
				endecrypted_file_content[endecrypted_file_content.Length - 2 - j] != '$' &&
				endecrypted_file_content[endecrypted_file_content.Length - 3 - j] != '$')
			{
				if (endecrypted_file_content.Length - 1 - j == 0) { filename_extension = ""; break; }
				filename_extension = filename_extension.Insert(0, endecrypted_file_content[endecrypted_file_content.Length - 1 - j].ToString());
				j++;
			}

			if (filename_extension != "")
			{
				filename_extension = filename_extension.Insert(0, endecrypted_file_content[endecrypted_file_content.Length - 1 - j].ToString());
				filename_extension = filename_extension.Insert(0, endecrypted_file_content[endecrypted_file_content.Length - 2 - j].ToString());
			}
			/*j = 0;
			filename_extension = "";
			while (endecrypted_file_content[endecrypted_file_content.Length - 1- j] != '.')
			{
				if (endecrypted_file_content.Length - 1 - j == 0) { filename_extension = ""; break; }
				//filename_extension = filename_extension.Insert(0, endecrypted_file_content[endecrypted_file_content.Length - 1 - j].ToString());
				j++;
			}*/

			new_file_path += filename_extension;
			FileStream fs_create = File.Create(new_file_path);
			fs_create.Close();
			endecrypted_file_content = endecrypted_file_content.Remove(endecrypted_file_content.Length - j - 5, j + 5);
			File.WriteAllText(new_file_path, endecrypted_file_content, Encoding.Default);
			return;
		}



		private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			
		}

		private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
				if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
		}

		
			/*Console.WriteLine("Please enter a password to use:");
			string password = Console.ReadLine();
			Console.WriteLine("Please enter a string to encrypt:");
			string plaintext = Console.ReadLine();
			Console.WriteLine("");

			Console.WriteLine("Your encrypted string is:");
			string encryptedstring = StringCipher.Encrypt(plaintext, password);
			Console.WriteLine(encryptedstring);
			Console.WriteLine("");

			string password2 = Console.ReadLine();
			Console.WriteLine("Your decrypted string is:");
			string decryptedstring = StringCipher.Decrypt(encryptedstring, password2);
			Console.WriteLine(decryptedstring);
			Console.WriteLine("");

			Console.WriteLine("Press any key to exit...");
			Console.ReadLine();*/
		

		private void Panel1_DragLeave(object sender, EventArgs e)
		{
			panel1.BackColor = Color.LightBlue;
		}

		private void Panel1_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder(e.Graphics, this.panel1.ClientRectangle, Color.DarkBlue, ButtonBorderStyle.Solid);
		}

		private void ChangeStatus(string str, Color color) {
			label4.Text = str;
			label4.ForeColor = color;
		}
	}

	public static class StringCipher
	{
		// This constant is used to determine the keysize of the encryption algorithm in bits.
		// We divide this by 8 within the code below to get the equivalent number of bytes.
		private const int Keysize = 256;

		// This constant determines the number of iterations for the password bytes generation function.
		private const int DerivationIterations = 1000;

		public static string EncryptAES(string plainText, string passPhrase)
		{
			// Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
			// so that the same Salt and IV values can be used when decrypting.  
			var saltStringBytes = Generate256BitsOfRandomEntropy();
			var ivStringBytes = Generate256BitsOfRandomEntropy();
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
			{
				var keyBytes = password.GetBytes(Keysize / 8);
				using (var symmetricKey = new RijndaelManaged())
				{
					symmetricKey.BlockSize = 256;
					symmetricKey.Mode = CipherMode.CBC;
					symmetricKey.Padding = PaddingMode.PKCS7;
					using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
					{
						using (var memoryStream = new MemoryStream())
						{
							using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
							{
								cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
								cryptoStream.FlushFinalBlock();
								// Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
								var cipherTextBytes = saltStringBytes;
								cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
								cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
								memoryStream.Close();
								cryptoStream.Close();
								return Convert.ToBase64String(cipherTextBytes);
							}
						}
					}
				}
			}
		}

		public static string DecryptAES(string cipherText, string passPhrase)
		{
			// Get the complete stream of bytes that represent:
			// [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
			var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
			// Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
			var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
			// Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
			var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
			// Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
			var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

			using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
			{
				var keyBytes = password.GetBytes(Keysize / 8);
				using (var symmetricKey = new RijndaelManaged())
				{
					symmetricKey.BlockSize = 256;
					symmetricKey.Mode = CipherMode.CBC;
					symmetricKey.Padding = PaddingMode.PKCS7;
					using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
					{
						using (var memoryStream = new MemoryStream(cipherTextBytes))
						{
							using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
							{
								var plainTextBytes = new byte[cipherTextBytes.Length];
								var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
								memoryStream.Close();
								cryptoStream.Close();
								return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
							}
						}
					}
				}
			}
		}
		public static string EncryptDES(string message, string password)
		{
			// Encode message and password
			byte[] messageBytes = ASCIIEncoding.ASCII.GetBytes(message);
			byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(password);

			// Set encryption settings -- Use password for both key and init. vector
			DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
			ICryptoTransform transform = provider.CreateEncryptor(passwordBytes, passwordBytes);
			CryptoStreamMode mode = CryptoStreamMode.Write;

			// Set up streams and encrypt
			MemoryStream memStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
			cryptoStream.Write(messageBytes, 0, messageBytes.Length);
			cryptoStream.FlushFinalBlock();

			// Read the encrypted message from the memory stream
			byte[] encryptedMessageBytes = new byte[memStream.Length];
			memStream.Position = 0;
			memStream.Read(encryptedMessageBytes, 0, encryptedMessageBytes.Length);

			// Encode the encrypted message as base64 string
			string encryptedMessage = Convert.ToBase64String(encryptedMessageBytes);

			return encryptedMessage;
		}

		public static string DecryptDES(string encryptedMessage, string password)
		{
			// Convert encrypted message and password to bytes
			byte[] encryptedMessageBytes = Convert.FromBase64String(encryptedMessage);
			byte[] passwordBytes = ASCIIEncoding.ASCII.GetBytes(password);

			// Set encryption settings -- Use password for both key and init. vector
			DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
			ICryptoTransform transform = provider.CreateDecryptor(passwordBytes, passwordBytes);
			CryptoStreamMode mode = CryptoStreamMode.Write;

			// Set up streams and decrypt
			MemoryStream memStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
			cryptoStream.Write(encryptedMessageBytes, 0, encryptedMessageBytes.Length);
			cryptoStream.FlushFinalBlock();

			// Read decrypted message from memory stream
			byte[] decryptedMessageBytes = new byte[memStream.Length];
			memStream.Position = 0;
			memStream.Read(decryptedMessageBytes, 0, decryptedMessageBytes.Length);

			// Encode deencrypted binary data to base64 string
			string message = ASCIIEncoding.ASCII.GetString(decryptedMessageBytes);

			return message;
		}

		public static string GenerateKey() {
			Random rnd = new Random();
			string key = "";
			for (int i = 0; i < rnd.Next(5, 10); i++) {
				key += (char)(rnd.Next(20,255));
			}
			return key;
		}

		public static byte[] Generate256BitsOfRandomEntropy()
		{
			var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
			using (var rngCsp = new RNGCryptoServiceProvider())
			{
				// Fill the array with cryptographically secure random bytes.
				rngCsp.GetBytes(randomBytes);
			}
			return randomBytes;
		}
	}

}
