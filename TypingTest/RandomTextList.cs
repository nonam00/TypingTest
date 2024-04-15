namespace TypingTest
{
    public static class RandomTextList
    {
        private static readonly IReadOnlyList<string> _list = new List<string>()
        {
            "Anyway, the presiding judge said he was going to proceed with the calling of witnesses. " +
            "The bailiff read off some names that caught my attention. " +
            "In the middle of what until then had been a shapeless mass of spectators, " +
            "I saw them stand up one by one, only to disappear again through a side door: " +
            "the director and the caretaker from the home, old Thomas Perez, Raymond, Masson, Salamano, and Marie. " +
            "She waved to me, anxiously. I was still feeling surprised that I hadn't seen them before when Celeste, " +
            "the last to be called, stood up. I recognized next to him the little woman from the restaurant, " +
            "with her jacket and her stiff and determined manner. She was staring right at me. " +
            "But I didn't have time to think about them, because the presiding judge started speaking. " +
            "He said that the formal proceedings were about to begin and that he didn't think he needed to remind the public to remain silent. " +
            "According to him, he was there to conduct in an impartial manner the proceedings of a case which he would consider objectively. " +
            "The verdict returned by the jury would be taken in a spirit of justice, and, in any event, " +
            "he would have the courtroom cleared at the slightest disturbance.",

            "The first mention of coffee in Brazil dates back to 1727. " +
            "Then the Portuguese Francisco de Melo Palheta brought plants and seeds to the north of the country from neighboring French Guiana. " +
            "It was the Typica, the ancestor of most Arabica varieties throughout the world. " +
            "It is still cultivated in Latin American countries - in Peru, the Dominican Republic and Jamaica. " +
            "However, this variety is susceptible to pests and diseases and produces low yields.",

            "Next.js is an open-source web development framework created by the private company Vercel " +
            "providing React-based web applications with server-side rendering and static website generation. " +
            "React documentation mentions Next.js among \"Recommended Toolchains\" " +
            "advising it to developers when \"building a server-rendered website with Node.js\". " +
            "Where traditional React apps can only render their content in the client-side browser, " +
            "Next.js extends this functionality to include applications rendered on the server-side."
        };

        private static Random random = new Random();
        public static string GetRandomText() => _list[random.Next(0, _list.Count)];
    }
}
