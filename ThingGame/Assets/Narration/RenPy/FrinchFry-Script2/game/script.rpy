# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.
define ff = Character('Frinchfry', color="#c8ffc8")


# The game starts here.
label start:
    play voice "ff-hubworld2-0.wav"
    ff "Woah! Where did you go? I was worried!"
    pause 0.8

label line2:
    play voice "ff-hubworld2-1.wav"
    ff  "...What? If it wants us to find it, then why doesn’t it just tell us where it is?"
    pause 0.8

label line3:
    play voice "ff-hubworld2-2.wav"
    ff "That guard said King Grumpybeard may know, but the Throne Room is like nine levels away..."
    pause 0.2

label line4:
    play voice "ff-hubworld2-3.wav"
    ff "Oh, wait! Eureka!"
    pause 0.2

label line5:
    play voice "ff-hubworld2-4.wav"
    ff "The cheat code! Remember when we found out how to make the castle bridge appear in the courtyard?"
    pause 0.2

label line6:
    play voice "ff-hubworld2-5.wav"
    ff "We skipped our way to level 10! Undefeated record time to this day! No one ever knew."
    pause 0.2

label line7:
    play voice "ff-hubworld2-6.wav"
    ff "You don’t remember the code? That’s a problem. Neither do I. That thing’s gonna break down the door unless we get rid of its source soon."
    pause 0.4

label line8:
    play voice "ff-hubworld2-6-2.wav"
    ff "Hurry up and go to the Courtyard. Maybe something there will jog your memory!"
    pause 0.2

label end:

    return
