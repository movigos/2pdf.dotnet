namespace ToPdf.Examples
{
    public static class HtmlSource
    {
        public static string HtmlContent = @"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Test Page for HTML to PDF Conversion</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f0f0;
            margin: 0;
            padding: 20px;
        }

        .container {
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            padding: 20px;
            border-radius: 8px;
        }

        h1 {
            color: #333;
        }

        .button {
            display: inline-block;
            background-color: #28a745;
            color: white;
            padding: 10px 15px;
            text-decoration: none;
            border-radius: 5px;
            cursor: pointer;
        }

        .button:hover {
            background-color: #218838;
        }

        .hidden-message {
            display: none;
            color: #555;
            margin-top: 20px;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1>Welcome to the HTML to PDF Test Page</h1>
        <p>This is a sample page to demonstrate HTML to PDF conversion.</p>
        <p>Click the button below to reveal a hidden message:</p>
        <a class='button' id='revealButton'>Reveal Message</a>
        <p class='hidden-message' id='hiddenMessage'>Congratulations! You've revealed the hidden message.</p>
    </div>
    <script>
        document.getElementById('revealButton').addEventListener('click', function() {
            var message = document.getElementById('hiddenMessage');
            message.style.display = 'block';
        });
    </script>
</body>
</html>
";

    }
}