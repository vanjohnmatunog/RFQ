<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>1st Birthday & Christening Invitation</title>

<style>
    body {
        margin: 0;
        font-family: 'Segoe UI', sans-serif;
        background: linear-gradient(135deg, #ffe6f0, #e6f7ff);
        color: #333;
        text-align: center;
    }

    .container {
        max-width: 800px;
        margin: 50px auto;
        background: white;
        border-radius: 20px;
        box-shadow: 0 10px 30px rgba(0,0,0,0.1);
        overflow: hidden;
    }

    header {
        background: linear-gradient(135deg, #ff9a9e, #fad0c4);
        padding: 40px 20px;
        color: white;
    }

    header h1 {
        margin: 0;
        font-size: 40px;
    }

    header h2 {
        margin: 10px 0 0;
        font-weight: normal;
    }

    .content {
        padding: 30px;
    }

    .baby-name {
        font-size: 32px;
        font-weight: bold;
        color: #ff4d6d;
        margin-bottom: 10px;
    }

    .details {
        font-size: 18px;
        line-height: 1.8;
    }

    .countdown {
        font-size: 24px;
        margin-top: 20px;
        color: #0077b6;
        font-weight: bold;
    }

    .rsvp {
        margin-top: 30px;
        padding: 20px;
        background: #f8f9fa;
        border-radius: 10px;
    }

    input, button {
        padding: 10px;
        margin: 5px;
        border-radius: 8px;
        border: 1px solid #ccc;
    }

    button {
        background: #ff4d6d;
        color: white;
        cursor: pointer;
        border: none;
    }

    button:hover {
        background: #e63950;
    }

    footer {
        padding: 20px;
        font-size: 14px;
        background: #fafafa;
    }
</style>
</head>

<body>

<div class="container">

    <header>
        <h1>You're Invited!</h1>
        <h2>1st Birthday & Christening Celebration</h2>
    </header>

    <div class="content">
        <div class="baby-name">Baby Liam</div>

        <div class="details">
            We are joyful to invite you to celebrate<br>
            the Christening and 1st Birthday of our little angel.<br><br>

            📅 Date: <b>June 15, 2026</b><br>
            🕒 Time: <b>10:00 AM</b><br>
            📍 Venue: <b>St. Mary’s Church & Garden Hall</b>
        </div>

        <div class="countdown" id="countdown"></div>

        <div class="rsvp">
            <h3>RSVP</h3>
            <input type="text" id="name" placeholder="Your Name">
            <button onclick="submitRSVP()">Confirm Attendance</button>
            <p id="message"></p>
        </div>
    </div>

    <footer>
        With love from the family ❤️
    </footer>

</div>

<script>
    // Countdown Timer
    const eventDate = new Date("June 15, 2026 10:00:00").getTime();

    const countdown = setInterval(function() {
        let now = new Date().getTime();
        let distance = eventDate - now;

        let days = Math.floor(distance / (1000 * 60 * 60 * 24));
        let hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        let minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        let seconds = Math.floor((distance % (1000 * 60)) / 1000);

        document.getElementById("countdown").innerHTML =
            "Countdown: " + days + "d " + hours + "h " + minutes + "m " + seconds + "s";

        if (distance < 0) {
            clearInterval(countdown);
            document.getElementById("countdown").innerHTML = "🎉 Event Started!";
        }
    }, 1000);

    // RSVP Function
    function submitRSVP() {
        let name = document.getElementById("name").value;
        if (name.trim() === "") {
            document.getElementById("message").innerHTML = "Please enter your name.";
        } else {
            document.getElementById("message").innerHTML =
                "Thank you, " + name + "! Your RSVP is confirmed ❤️";
        }
    }
</script>

</body>
</html>
