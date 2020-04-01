using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentSearchNS;

namespace DocumentSearchTests
{
    [TestClass]
    public class DocumentSearchTests
    {
        private readonly string testData = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore " +
                                            "et dolore magna aliqua. Ullamcorper malesuada proin libero nunc. Suscipit adipiscing bibendum est ultricies " +
                                            "integer quis. Urna et pharetra pharetra massa massa ultricies mi. Nibh ipsum consequat nisl vel pretium lectus " +
                                            "quam id leo. Risus viverra adipiscing at in tellus integer feugiat scelerisque varius. Ut eu sem integer vitae " +
                                            "justo eget magna. Dignissim cras tincidunt lobortis feugiat vivamus. Vel pretium lectus quam id leo. Urna et " +
                                            "pharetra pharetra massa. Pretium quam vulputate dignissim suspendisse in est ante in nibh. Condimentum lacinia " +
                                            "quis vel eros donec ac odio. Varius sit amet mattis vulputate enim nulla aliquet porttitor lacus. Eget nullam " +
                                            "non nisi est. Bibendum neque egestas congue quisque egestas diam in. Nisl pretium fusce id velit ut tortor " +
                                            "pretium viverra.Tincidunt nunc pulvinar sapien et ligula ullamcorper malesuada proin libero.Tempus urna et " +
                                            "pharetra pharetra massa. Arcu dictum varius duis at consectetur lorem donec. Lectus vestibulum mattis ullamcorper " +
                                            "velit sed ullamcorper.Gravida rutrum quisque non tellus.Consequat ac felis donec et odio pellentesque diam volutpat " +
                                            "commodo. Tellus cras adipiscing enim eu turpis egestas pretium aenean.Eu lobortis elementum nibh tellus molestie " +
                                            "nunc non blandit massa. Parturient montes nascetur ridiculus mus mauris vitae ultricies. Scelerisque viverra mauris " +
                                            "in aliquam sem fringilla ut morbi.Porttitor massa id neque aliquam vestibulum. Convallis convallis tellus id interdum " +
                                            "velit laoreet id donec.Ac turpis egestas sed tempus urna et pharetra. Purus in massa tempor nec feugiat nisl pretium. " +
                                            "Facilisis mauris sit amet massa vitae tortor condimentum lacinia.Eu feugiat pretium nibh ipsum consequat nisl vel. " +
                                            "Nisl purus in mollis nunc sed.Ut tortor pretium viverra suspendisse.Egestas maecenas pharetra convallis posuere morbi. " +
                                            "Dignissim enim sit amet venenatis urna. Id faucibus nisl tincidunt eget nullam non nisi est.Diam ut venenatis tellus in. " +
                                            "Praesent semper feugiat nibh sed pulvinar proin gravida hendrerit.Aliquam sem et tortor consequat.Semper feugiat nibh " +
                                            "sed pulvinar.Quam elementum pulvinar etiam non quam lacus suspendisse faucibus interdum. Purus semper eget duis at. " +
                                            "Maecenas pharetra convallis posuere morbi leo urna. Aliquet porttitor lacus luctus accumsan tortor. Vitae justo eget " +
                                            "magna fermentum iaculis eu non diam.Convallis a cras semper auctor neque vitae tempus quam.Morbi tincidunt ornare massa " +
                                            "eget egestas purus viverra. At tempor commodo ullamcorper a lacus vestibulum sed arcu.Id venenatis a condimentum vitae. " +
                                            "Adipiscing bibendum est ultricies integer quis auctor elit. Nunc sed blandit libero volutpat sed cras ornare. Amet massa " +
                                            "vitae tortor condimentum lacinia quis.Mauris pellentesque pulvinar pellentesque habitant morbi tristique senectus et netus. " +
                                            "Malesuada bibendum arcu vitae elementum curabitur vitae nunc. Urna condimentum mattis pellentesque id nibh tortor.Curabitur " +
                                            "gravida arcu ac tortor dignissim. Magna fringilla urna porttitor rhoncus dolor purus non enim praesent. In hac habitasse " +
                                            "platea dictumst quisque sagittis purus. Platea dictumst quisque sagittis purus sit amet volutpat consequat.Adipiscing commodo " +
                                            "elit at imperdiet dui accumsan sit. Feugiat nibh sed pulvinar proin gravida. Blandit libero volutpat sed cras ornare. Nisi porta " +
                                            "lorem mollis aliquam.Vel pharetra vel turpis nunc.Risus pretium quam vulputate dignissim suspendisse in. Nunc sed velit dignissim " +
                                            "sodales ut eu sem integer.Eget magna fermentum iaculis eu non diam phasellus. Pellentesque nec nam aliquam sem et tortor. Euismod " +
                                            "lacinia at quis risus.Lacinia quis vel eros donec ac. Semper quis lectus nulla at volutpat diam.Libero justo laoreet sit amet. Sit " +
                                            "amet massa vitae tortor condimentum lacinia.Proin libero nunc consequat interdum.Posuere sollicitudin aliquam ultrices sagittis orci " +
                                            "a. Massa placerat duis ultricies lacus sed turpis tincidunt. Urna et pharetra pharetra massa massa ultricies mi. Pellentesque id nibh " +
                                            "tortor id aliquet lectus proin.";

        [TestMethod]
        public void StringSearchTest()
        {
            // Arrange
            var termToTest = "massa";
            var totalCount = 14;

            // Act
            int result = DocumentSearch.StringSearch(testData, termToTest);

            // Assert
            Assert.AreEqual(result, totalCount);
        }

        [TestMethod]
        public void RegexSearchTest()
        {
            // Arrange
            var termToTest = "massa";
            var totalCount = 14;

            // Act
            int result = DocumentSearch.RegexSearch(testData, termToTest);

            // Assert
            Assert.AreEqual(result, totalCount);
        }

        [TestMethod]
        public void IndexedSearchTest()
        {
            // Arrange
            var termToTest = "massa";
            var totalCount = 14;

            // Act
            int result = DocumentSearch.IndexedSearch(testData, termToTest);

            // Assert
            Assert.AreEqual(result, totalCount);
        }
    }
}
