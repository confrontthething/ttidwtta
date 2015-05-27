# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define ff = Character('Frinchfry', color="#c8ffc8")

# The game starts here.
label start:
    play voice "ff-hubworld2-14.wav"
    ff "What are you waiting for? Shoot it! The rest is gonna get through any second now!"
    pause 0.2
label line2:
    play voice "ff-hubworld2-15.wav"
    ff "Burn! Burn! Burn!"

label end:
    return
