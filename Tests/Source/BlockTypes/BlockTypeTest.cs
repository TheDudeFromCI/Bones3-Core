using Bones3Rebuilt;
using Bones3Rebuilt.BlockTypes;
using Moq;
using NUnit.Framework;

namespace Test
{
    public class BlockTypeTest
    {
        [Test]
        public void DefaultProperties()
        {
            var blockType = new BlockBuilder(3).Build();

            Assert.AreEqual("New Block", blockType.Name);
            Assert.AreEqual(3, blockType.ID);
            Assert.IsTrue(blockType.IsVisible);
            Assert.IsTrue(blockType.IsSolid);
            Assert.AreEqual("Block:[3) New Block]", blockType.ToString());

            for (int i = 0; i < 6; i++)
            {
                var face = blockType.Face(i);

                Assert.AreEqual(i, face.Side);
                Assert.AreEqual(FaceRotation.Normal, face.Rotation);
                Assert.AreEqual(null, face.Texture);
            }
        }

        [Test]
        public void CustomProperties()
        {
            var texture = new Mock<IBlockTexture>();

            var blockType = new BlockBuilder(7)
                .Name("Grass")
                .Solid(false)
                .Visible(false)
                .FaceRotation(2, FaceRotation.MirroredClockwise270)
                .FaceRotation(4, FaceRotation.Mirrored)
                .Texture(3, texture.Object)
                .Build();

            Assert.AreEqual("Grass", blockType.Name);
            Assert.AreEqual(7, blockType.ID);
            Assert.IsFalse(blockType.IsVisible);
            Assert.IsFalse(blockType.IsSolid);

            Assert.AreEqual(FaceRotation.MirroredClockwise270, blockType.Face(2).Rotation);
            Assert.AreEqual(FaceRotation.Mirrored, blockType.Face(4).Rotation);
            Assert.AreEqual(texture.Object, blockType.Face(3).Texture);
        }
    }
}
