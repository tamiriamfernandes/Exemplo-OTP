// Gerar chave secreta aleatória
using OtpNet;
using QRCoder;

var secretKey = KeyGeneration.GenerateRandomKey(20);
var base32Secret = Base32Encoding.ToString(secretKey); // Armazenar no banco

// Criar URL para QR code (compatível com Google Authenticator)
string issuer = "ExemploOtp"; //Nome da aplicação
string account = "email@exemplo.com";
string otpUrl = $"otpauth://totp/{issuer}:{account}?secret={base32Secret}&issuer={issuer}";

// Gerar QR Code (opcional, para exibir na tela)
QRCodeGenerator qrGenerator = new QRCodeGenerator();
QRCodeData qrCodeData = qrGenerator.CreateQrCode(otpUrl, QRCodeGenerator.ECCLevel.Q);
Base64QRCode qrCode = new Base64QRCode(qrCodeData);
string qrCodeBase64 = qrCode.GetGraphic(20); // pode exibir como <img src="data:image/png;base64,{qrCodeBase64}">
Console.WriteLine(qrCodeBase64);

// Suponha que o usuário digitou o código 6 dígitos (ex: 123456)
string userInputCode = "123456";

// Recuperar chave secreta armazenada no cadastro
var secretKey2 = Base32Encoding.ToBytes(base32Secret);
var totp = new Totp(secretKey2);

bool isValid = totp.VerifyTotp(userInputCode, out long timeStepMatched, VerificationWindow.RfcSpecifiedNetworkDelay);

// Resultado
if (isValid)
{
    Console.WriteLine("Código OTP válido");
}
else
{
    Console.WriteLine("Código inválido ou expirado");
}
