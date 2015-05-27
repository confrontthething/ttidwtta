using UnityEngine;
using NUnit.Framework;

namespace ThingGameTest {
  [TestFixture]
  [Category("Logic Tests")]
	internal class SimonSaysTests {

		System.Random rand;

		public SimonSaysTests() {
			rand = new System.Random();
		}

		[Test]
		[Repeat(10)]
		public void GeneratePuzzleTest([NUnit.Framework.Range(1, 40, 1)] int x)
		{
			SimonSays.Gemstone[] puzzle = {};
			SimonSays.GeneratePuzzle(ref puzzle, x, rand);

			if (puzzle.Length != x) {
				Assert.Fail("puzzle length did not match difficulty");
			}

			for (int i = 0; i < puzzle.Length; i++) {
				SimonSays.Gemstone g = puzzle[i];
				if (!System.Enum.IsDefined(typeof(SimonSays.Gemstone), g)) {
					Assert.Fail("gemstone enum not valid");
				}

				if (i >= 2) {
					SimonSays.Gemstone g1 = puzzle[i - 1];
					SimonSays.Gemstone g2 = puzzle[i - 2];

					if (g ==  g1 && g1 == g2) {
						Assert.Fail("three in a row");
					}
				}
			}
		}

	}
}
