# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define p = Character('Player', color="#ff00ff")

# The game starts here.
label start:

    play voice "fivesec-silence-track.wav"
    p "Sprawling city, bright nights, heading home from a long day... I fell asleep on the bus."

    play voice "fivesec-silence-track.wav"
    p "Dad carried me off at our stop, but he didn't see Frinchfry on the floor. Left behind."

    play voice "fivesec-silence-track.wav"
    p "I felt terrible watching the bus disappear into the distance, but I didn't want to cry in front of him."

    play voice "fivesec-silence-track.wav"
    p "I didn't think he'd care, but he got a hold of the driver, and we waited for Frinchfry's return."

label end:
    return
