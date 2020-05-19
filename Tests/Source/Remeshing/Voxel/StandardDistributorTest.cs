using Bones3Rebuilt.Remeshing.Voxel;
using Bones3Rebuilt.Remeshing;
using Bones3Rebuilt;

using Moq;

using NUnit.Framework;

namespace Test
{
    public class StandardDistributorTest
    {
        [Test]
        public void ChunkAllAir_GeneratesNoTasks()
        {
            var dis = new StandardDistributor();
            var props = new Mock<ChunkProperties>();
            props.Setup(p => p.ChunkSize).Returns(new GridSize(2));

            var block = new BlockBuilder(1)
                .Name("Air")
                .Solid(false)
                .Visible(false)
                .Build();

            props.Setup(p => p.GetBlock(It.IsAny<BlockPosition>())).Returns(block);

            var taskStack = new RemeshTaskStack(default);
            dis.CreateTasks(props.Object, taskStack);

            var report = taskStack.ToReport();
            Assert.AreEqual(0, report.VisualMesh.TotalLayers);
            Assert.AreEqual(0, report.CollisionMesh.TotalLayers);
        }

        [Test]
        public void SomeVisible_NoSolid()
        {
            var dis = new StandardDistributor();
            var props = new Mock<ChunkProperties>();
            props.Setup(p => p.ChunkSize).Returns(new GridSize(3));

            var air = new BlockBuilder(1)
                .Name("Air")
                .Solid(false)
                .Visible(false)
                .Build();

            var tallGrass = new BlockBuilder(13)
                .Name("Tall Grass")
                .Solid(false)
                .Visible(true)
                .Texture(0, NewTexture())
                .Build();

            props.Setup(p => p.GetBlock(It.IsAny<BlockPosition>())).
            Returns<BlockPosition>(pos =>
            {
                if (pos.Z == 5)
                    return tallGrass;
                else
                    return air;
            });

            props.Setup(p => p.GetNextBlock(It.IsAny<BlockPosition>(), It.IsAny<int>())).
            Returns<BlockPosition, int>((pos, side) =>
            {
                pos.ShiftAlongDirection(side);

                if (pos.Z == 5)
                    return tallGrass;
                else
                    return air;
            });

            var taskStack = new RemeshTaskStack(default);
            dis.CreateTasks(props.Object, taskStack);

            var report = taskStack.ToReport();
            Assert.AreEqual(1, report.VisualMesh.TotalLayers);
            Assert.AreEqual(0, report.CollisionMesh.TotalLayers);
        }

        [Test]
        public void SomeSolid_NoVisible()
        {
            var dis = new StandardDistributor();
            var props = new Mock<ChunkProperties>();
            props.Setup(p => p.ChunkSize).Returns(new GridSize(6));

            var air = new BlockBuilder(1)
                .Name("Air")
                .Solid(false)
                .Visible(false)
                .Build();

            var wall = new BlockBuilder(3)
                .Name("Invisible Wall")
                .Solid(true)
                .Visible(false)
                .Build();

            props.Setup(p => p.GetBlock(It.IsAny<BlockPosition>())).
            Returns<BlockPosition>(pos =>
            {
                if (pos.X == 7)
                    return wall;
                else
                    return air;
            });

            var taskStack = new RemeshTaskStack(default);
            dis.CreateTasks(props.Object, taskStack);

            var report = taskStack.ToReport();
            Assert.AreEqual(0, report.VisualMesh.TotalLayers);
            Assert.AreEqual(1, report.CollisionMesh.TotalLayers);
        }

        [Test]
        public void StandardChunk()
        {
            var dis = new StandardDistributor();
            var props = new Mock<ChunkProperties>();
            props.Setup(p => p.ChunkSize).Returns(new GridSize(4));

            var air = new BlockBuilder(1)
                .Name("Air")
                .Solid(false)
                .Visible(false)
                .Build();

            var grass = new BlockBuilder(5)
                .Name("Grass")
                .Solid(true)
                .Visible(true)
                .Texture(0, NewTexture())
                .Build();

            props.Setup(p => p.GetBlock(It.IsAny<BlockPosition>())).
            Returns<BlockPosition>(pos =>
            {
                if (pos.Y == 1)
                    return grass;
                else
                    return air;
            });

            props.Setup(p => p.GetNextBlock(It.IsAny<BlockPosition>(), It.IsAny<int>())).
            Returns<BlockPosition, int>((pos, side) =>
            {
                pos.ShiftAlongDirection(side);

                if (pos.Y == 1)
                    return grass;
                else
                    return air;
            });

            var taskStack = new RemeshTaskStack(default);
            dis.CreateTasks(props.Object, taskStack);

            var report = taskStack.ToReport();
            Assert.AreEqual(1, report.VisualMesh.TotalLayers);
            Assert.AreEqual(1, report.CollisionMesh.TotalLayers);
        }

        private IBlockTexture NewTexture()
        {
            var texture = new Mock<IBlockTexture>();
            var atlas = new Mock<ITextureAtlas>();

            texture.Setup(t => t.Atlas).Returns(atlas.Object);

            return texture.Object;
        }

        [Test]
        public void ThreeInputsAtlases_ThreeOutputLayers()
        {
            var dis = new StandardDistributor();
            var props = new Mock<ChunkProperties>();
            props.Setup(p => p.ChunkSize).Returns(new GridSize(4));

            var air = new BlockBuilder(1)
                .Name("Air")
                .Solid(false)
                .Visible(false)
                .Build();

            var grass = new BlockBuilder(5)
                .Name("Grass")
                .Solid(true)
                .Visible(true)
                .Texture(0, NewTexture())
                .Texture(1, NewTexture())
                .Texture(2, NewTexture())
                .Build();

            props.Setup(p => p.GetBlock(It.IsAny<BlockPosition>())).
            Returns<BlockPosition>(pos =>
            {
                if (pos.Y == 1)
                    return grass;
                else
                    return air;
            });

            var taskStack = new RemeshTaskStack(default);
            dis.CreateTasks(props.Object, taskStack);

            var report = taskStack.ToReport();
            Assert.AreEqual(3, report.VisualMesh.TotalLayers);
            Assert.AreEqual(1, report.CollisionMesh.TotalLayers);
        }
    }
}
