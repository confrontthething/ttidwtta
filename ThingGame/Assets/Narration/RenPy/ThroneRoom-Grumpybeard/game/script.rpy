# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define ff = Character('Frinchfry', color="#c8ffc8")
define gb = Character('King Grumpybeard', color="#87ceeb")

# The game starts here.
label start:
    play voice "ff-hubworld2-12.wav"
    ff "Holy Toledo. It’s the King!"

    play voice "kg-line1.wav"
    gb "Oh, hello there. I haven’t seen you in ages!"
    pause 0.2

    play voice "kg-line2.wav"
    gb "I suppose you’re wondering what I’m doing floating up here. Well, I’ll never tell!"
    pause 0.2

    play voice "kg-line3.wav"
    gb "That man-shadow-thing just flew in here and really made himself at home. Not that I mind too much. I’m quite comfortable up here."
    pause 0.2

    play voice "kg-line4.wav"
    gb "The quiet’s been very nice actually. However, I’m getting quite warm in this room."
    pause 0.2

    play voice "kg-line5.wav"
    gb "Maybe find a way to get me down? I’d start by talking with that fellow sitting on my throne."
    pause 0.4

    play voice "ff-hubworld2-13.wav"
    ff "Whoa, he’s a lot less grumpy when he’s incapacitated."

label end:
    return
