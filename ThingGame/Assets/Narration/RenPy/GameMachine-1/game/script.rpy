# You can place the script of your game in this file.

# Declare images below this line, using the image statement.
# eg. image eileen happy = "eileen_happy.png"

# Declare characters used by this game.

define sample = Character('Potato', color="#ff00ff")
define p = Character('Player', color="#ff00ff")

# The game starts here.
label start:

    play voice "fivesec-silence-track.wav"
    p "Warm breezes, summer sounds, and a full tummy... That was the day I met Frinchfry. Dad won that carnival game for me, and they said I could have any prize."

    play voice "fivesec-silence-track.wav"
    p "I saw big ones and small ones, but the only one I wanted was Frinchfry."

    play voice "fivesec-silence-track.wav"
    p "We played together, and fell asleep on the blanket that night. Frinchfry was someone who I knew would always be with me..."

    play voice "fivesec-silence-track.wav"
    p "Just like dad would be."

label end:
    return
