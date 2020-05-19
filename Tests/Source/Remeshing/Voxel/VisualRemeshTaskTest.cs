using Bones3Rebuilt;

using Moq;

using NUnit.Framework;

namespace Test
{
    public class VisualRemeshTaskTest
    {
        [Test]
        public void TwoCubes()
        {
            var chunkSize = new GridSize(3);
            var chunkPosition = new ChunkPosition(0, 0, 0);
            var chunk = new Chunk(chunkSize, chunkPosition);

            chunk.SetBlockID(new BlockPosition(0, 0, 0), 2);
            chunk.SetBlockID(new BlockPosition(0, 1, 0), 2);

            var world = new Mock<IBlockContainerProvider>();
            world.Setup(w => w.GetContainer(chunkPosition, It.IsAny<bool>())).Returns(chunk);
            world.Setup(w => w.ContainerSize).Returns(chunkSize);

            BlockTypeList blockList = new BlockTypeList();
            blockList.AddBlockType(new BlockBuilder(2)
                .Name("Grass")
                .Solid(true)
                .Visible(true)
                .Build());

            var props = new ChunkProperties(world.Object, chunkPosition, blockList);

            var task = new VisualRemeshTask(props, null);
            task.Finish();

            Assert.AreEqual(24, task.Mesh.Vertices.Count);
            Assert.AreEqual(24, task.Mesh.Normals.Count);
            Assert.AreEqual(24, task.Mesh.UVs.Count);
        }

        [Test]
        public void RespectNeighborChunks()
        {
            var chunkSize = new GridSize(3);
            var chunkPosition = new ChunkPosition(0, 0, 0);
            var chunk = new Chunk(chunkSize, chunkPosition);

            var neighborPosition = new ChunkPosition(-1, 0, 0);
            var neighbor = new Chunk(chunkSize, neighborPosition);

            chunk.SetBlockID(new BlockPosition(0, 0, 0), 2);
            neighbor.SetBlockID(new BlockPosition(7, 0, 0), 2);

            var world = new Mock<IBlockContainerProvider>();
            world.Setup(w => w.GetContainer(chunkPosition, It.IsAny<bool>())).Returns(chunk);
            world.Setup(w => w.GetContainer(neighborPosition, It.IsAny<bool>())).Returns(neighbor);
            world.Setup(w => w.ContainerSize).Returns(chunkSize);

            BlockTypeList blockList = new BlockTypeList();
            blockList.AddBlockType(new BlockBuilder(2)
                .Name("Grass")
                .Solid(true)
                .Visible(true)
                .Build());

            var props = new ChunkProperties(world.Object, chunkPosition, blockList);

            var task = new VisualRemeshTask(props, null);
            task.Finish();

            Assert.AreEqual(20, task.Mesh.Vertices.Count);
            Assert.AreEqual(20, task.Mesh.Normals.Count);
            Assert.AreEqual(20, task.Mesh.UVs.Count);
        }

        private IBlockTexture NewTexture()
        {
            var texture = new Mock<IBlockTexture>();
            var atlas = new Mock<ITextureAtlas>();

            texture.Setup(t => t.Atlas).Returns(atlas.Object);

            return texture.Object;
        }

        [Test]
        public void BlockFace_DifferentAtlas_IgnoreQuads()
        {
            var chunkSize = new GridSize(3);
            var chunkPosition = new ChunkPosition(0, 0, 0);
            var chunk = new Chunk(chunkSize, chunkPosition);

            chunk.SetBlockID(new BlockPosition(0, 0, 0), 2);

            var world = new Mock<IBlockContainerProvider>();
            world.Setup(w => w.GetContainer(chunkPosition, It.IsAny<bool>())).Returns(chunk);
            world.Setup(w => w.ContainerSize).Returns(chunkSize);

            var texture1 = NewTexture();
            var texture2 = NewTexture();

            BlockTypeList blockList = new BlockTypeList();
            blockList.AddBlockType(new BlockBuilder(2)
                .Name("Grass")
                .Solid(true)
                .Visible(true)
                .Texture(0, texture1)
                .Texture(1, texture1)
                .Texture(2, texture2)
                .Texture(3, texture2)
                .Texture(4, texture2)
                .Texture(5, texture2)
                .Build());

            var props = new ChunkProperties(world.Object, chunkPosition, blockList);

            var task = new VisualRemeshTask(props, texture2.Atlas);
            task.Finish();

            Assert.AreEqual(16, task.Mesh.Vertices.Count);
        }
    }
}
