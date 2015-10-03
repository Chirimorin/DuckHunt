using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DuckHunt.Factories
{
    public class SpriteSheetFactory
    {
        #region Lazy singleton & constructor
        private static readonly Lazy<SpriteSheetFactory> _instance
            = new Lazy<SpriteSheetFactory>(() => new SpriteSheetFactory());

        // private to prevent direct instantiation.
        private SpriteSheetFactory()
        {
            _assembly = typeof(SpriteSheetFactory).Assembly;
            _spriteSheets = new Dictionary<string, BitmapSource[]>();
            _spriteSheetInfos = new Dictionary<string, SpriteSheetInfo>();

            using (Stream stream = _assembly.GetManifestResourceStream("DuckHunt.SpriteSheets.SpriteSheetsInfo.txt"))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = line = reader.ReadLine().Split(';');
                    if (line.Length < 3)
                        throw new FileFormatException("Error tijdens het lezen van SpriteSheetsInfo.txt (regel had " + line.Length + " elementen, 3 verwacht");
                    string fileName = line[0];
                    int xImages = int.Parse(line[1]);
                    int yImages = int.Parse(line[2]);

                    _spriteSheetInfos.Add(fileName, new SpriteSheetInfo(fileName, xImages, yImages));
                }
            }
        }

        // accessor for instance
        public static SpriteSheetFactory Instance
        {
            get { return _instance.Value; }
        }
        #endregion

        #region Variables
        private Assembly _assembly;

        /// <summary>
        /// Spritesheet infos uitgelezen uit het info .txt bestand
        /// </summary>
        private readonly Dictionary<string, SpriteSheetInfo> _spriteSheetInfos;

        /// <summary>
        /// Houd de ingeladen spritesheets bij, zodat ze maar 1 keer geladen hoeven te worden.
        /// </summary>
        private Dictionary<string, BitmapSource[]> _spriteSheets;
        #endregion

        #region public functies
        public BitmapSource[] GetSprites(string fileName)
        {
            BitmapSource[] result;

            // Als de spritesheet nog niet geladen is, laad hem nu
            if (!_spriteSheets.TryGetValue(fileName, out result))
            {
                result = LoadSprites(fileName);
                _spriteSheets.Add(fileName, result);
            }

            return result;
        }
        #endregion

        #region Sprites inladen
        /// <summary>
        /// Leest het spritesheet bestand en laad de spritesheet
        /// </summary>
        /// <param name="fileName">Naam van de sprite</param>
        /// <returns>Een array van sprites</returns>
        private BitmapSource[] LoadSprites(string fileName)
        {
            SpriteSheetInfo info;
            if (!_spriteSheetInfos.TryGetValue(fileName, out info))
                throw new ArgumentException("Spritesheet " + fileName + " heeft geen info");

            return ProcessSpriteSheet(info.FileName, info.XImages, info.YImages);
        }

        /// <summary>
        /// Laad de spritesheet image en snijd hem in losse sprites
        /// </summary>
        /// <param name="imageFile">Bestandsnaam van de spritesheet</param>
        /// <param name="xImages">Aantal horizontale images</param>
        /// <param name="yImages">Aantal verticale images</param>
        /// <param name="spriteWidth">Breedte van elke sprite</param>
        /// <param name="spriteHeight">Hoogte van elke sprite</param>
        /// <returns>Een array van sprites</returns>
        public BitmapSource[] ProcessSpriteSheet(string imageFile, int xImages, int yImages)
        {
            BitmapSource[] result = new BitmapSource[xImages * yImages];

            

            // Spritesheet inladen vanuit embedded resources
            System.Drawing.Bitmap src = new System.Drawing.Bitmap(_assembly.GetManifestResourceStream("DuckHunt.SpriteSheets." + imageFile));

            // Rechthoek aanmaken ter grootte van de sprites
            int spriteWidth = src.Width / xImages;
            int spriteHeight = src.Height / yImages;
            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(0, 0, spriteWidth, spriteHeight);

            // Target bitmap voor losse sprites
            System.Drawing.Bitmap target;

            for (int col = 0; col < xImages; col++)
            {
                for (int row = 0; row < yImages; row++)
                {
                    // Zoek de positie van de huidige sprite
                    int currentX = col * spriteWidth;
                    int currentY = row * spriteHeight;
                    cropRect.X = currentX;
                    cropRect.Y = currentY;

                    // Maak een bitmap om de losse sprite op te tekenen
                    target = new System.Drawing.Bitmap(cropRect.Width, cropRect.Height);

                    // Snij de sprite uit
                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(target))
                    {
                        g.DrawImage(src, new System.Drawing.Rectangle(0, 0, target.Width, target.Height), cropRect, System.Drawing.GraphicsUnit.Pixel);
                    }

                    // Zet de sprite om in een BitmapSource
                    BitmapSource frame = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(target.GetHbitmap(), IntPtr.Zero,
                    System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(target.Width, target.Height));

                    // Sla de sprite op in de array van sprites
                    int index = col + row * xImages;
                    result[index] = frame;
                }
            }

            return result;
        }
        #endregion

        // Container voor sprite sheet informatie.
        class SpriteSheetInfo
        {
            public string FileName { get; private set; }
            public int XImages     { get; private set; }
            public int YImages     { get; private set; }

            public SpriteSheetInfo(string fileName, int xImages, int yImages)
            {
                FileName = fileName;
                XImages = xImages;
                YImages = yImages;
            }
        }
    }
}
