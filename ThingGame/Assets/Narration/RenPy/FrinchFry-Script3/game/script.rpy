# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")


# The game starts here.
label start:


    play voice "ff-hubworld2-8.wav"
    ff "It spoke to you again? What did it say this time?"
    pause 0.2

label line2:
    play voice "ff-hubworld2-9.wav"
    ff "Oh? You didn’t understand it?"
    pause 0.2

label line3:
    play voice "ff-hubworld2-10.wav"
    ff "Good, you shouldn’t be listening to it anyway."
    pause 0.2

label line4:
    play voice "ff-hubworld2-11.wav"
    ff "...I'm trying as hard as I can to keep it out, but it’s getting stronger... Hurry and go to the Throne Room. King Grumpybeard should know where the source is."
    pause 0.4

label end:
    return
