using Bones3Rebuilt;

using NUnit.Framework;

namespace Test
{
    public class TextureAtlasTest
    {
        [Test]
        public void AddTexture()
        {
            var atlas = new TextureAtlas();
            var texture = atlas.AddTexture();

            Assert.IsNotNull(texture);
            Assert.AreEqual(atlas, texture.Atlas);
            Assert.AreEqual(0, texture.Index);
            Assert.AreEqual(texture, atlas.GetTexture(0));
        }

        [Test]
        public void MultipleTextures_CorrectIndices()
        {
            var atlas = new TextureAtlas();

            var texture0 = atlas.AddTexture();
            var texture1 = atlas.AddTexture();
            var texture2 = atlas.AddTexture();

            Assert.AreEqual(0, texture0.Index);
            Assert.AreEqual(1, texture1.Index);
            Assert.AreEqual(2, texture2.Index);

            Assert.AreEqual(texture0, atlas.GetTexture(0));
            Assert.AreEqual(texture1, atlas.GetTexture(1));
            Assert.AreEqual(texture2, atlas.GetTexture(2));
        }
    }
}
