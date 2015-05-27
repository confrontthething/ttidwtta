# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")
define me = Character('Me', color="#c8ffc8")


# The game starts here.
label start:
    play voice "ff-dungeon-4.wav"
    ff "Did you forget how to play, buddy? LEFT CLICK to shoot it with a fireball."
    pause 0.2
    
label line1:
    play voice "ff-dungeon-5.wav"
    ff "You can recharge mana at the fire pits! Use SPACE to jump."
    pause 0.2

label end:

    return
