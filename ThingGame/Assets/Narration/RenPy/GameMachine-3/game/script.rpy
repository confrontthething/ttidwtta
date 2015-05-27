# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define p = Character('Player', color="#ff00ff")

# The game starts here.
label start:

    play voice "fivesec-silence-track.wav"
    p "Grey clouds, closed doors, banana pancakes, pizza rolls..."

    play voice "fivesec-silence-track.wav"
    p "It was raining, and mom said we couldn't go outside. So we just sat and played “the floor is lava.”"

    play voice "fivesec-silence-track.wav"
    p "I fell off the couch, twisted my ankle, and started to cry."

    play voice "fivesec-silence-track.wav"
    p "Then I gave you a hug, and everything felt better."

label end:
    return
