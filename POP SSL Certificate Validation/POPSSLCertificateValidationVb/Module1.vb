Imports System
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports GemBox.Email
Imports GemBox.Email.Pop
Imports GemBox.Email.Security

Module Module1

    Sub Main()

        ' If using Professional version, put your serial key below.
        ComponentInfo.SetLicense("FREE-LIMITED-KEY")

        ' Define certificate validation delegate which will ignore
        ' 'Certificate name mismatch' errors
        Dim validationDelegate As RemoteCertificateValidationCallback =
            Function(sender As Object,
                     certificate As X509Certificate,
                     chain As X509Chain,
                     sslPolicyErrors As SslPolicyErrors) As Boolean

                If sslPolicyErrors = SslPolicyErrors.None OrElse
                   sslPolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch Then
                    Console.WriteLine("Server certificate is valid.")
                    Return True
                Else
                    Console.WriteLine("Server certificate is invalid. Errors: " &
                                      sslPolicyErrors.ToString())
                    Return False
                End If
            End Function

        ' Create new PopClient and specify IP port,
        ' security type And certificate validation callback
        Using pop As New PopClient("<ADDRESS> (e.g. pop.gmail.com)",
                                   995,
                                   ConnectionSecurity.Ssl,
                                   validationDelegate)

            ' Connect to mail server
            pop.Connect()
            Console.WriteLine("Connected.")

            ' Authenticate with specified username,
            ' password And authentication mechanism
            pop.Authenticate("<USERNAME>", "<PASSWORD>", PopAuthentication.Plain)
            Console.WriteLine("Authenticated.")
        End Using

    End Sub

End Module