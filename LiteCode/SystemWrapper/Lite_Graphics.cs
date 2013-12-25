using System;
using System.Collections.Generic;
using System.Text;
using LiteCode.SystemWrapper.Interfaces;
using System.Drawing;
using System.Windows.Forms;

namespace LiteCode.SystemWrapper
{
    public class Lite_Graphics : IGraphics
    {
        private Graphics g;
        public Lite_Graphics(Image image)
        {
            this.g = Graphics.FromImage(image);
        }
        public Lite_Graphics(IntPtr handle, GraphicsInit initType)
        {
            switch (initType)
            {
                case GraphicsInit.FromHdc:
                    this.g = Graphics.FromHdc(handle);
                    break;
                case GraphicsInit.FromHdcInternal:
                    this.g = Graphics.FromHdcInternal(handle);
                    break;
                case GraphicsInit.FromHwnd:
                    this.g = Graphics.FromHwnd(handle);
                    break;
                case GraphicsInit.FromHwndInternal:
                    this.g = Graphics.FromHwndInternal(handle);
                    break;
            }
        }
        public Lite_Graphics(IntPtr hdc, IntPtr hdevice)
        {
            this.g = Graphics.FromHdc(hdc, hdevice);
        }

        public Region Clip
        {
            [RemoteExecution]
            get { return g.Clip; }
            [RemoteExecution]
            set { g.Clip = value; }
        }

        public RectangleF ClipBounds
        {
            [RemoteExecution]
            get { return g.ClipBounds; }
        }

        public System.Drawing.Drawing2D.CompositingMode CompositingMode
        {
            [RemoteExecution]
            get { return g.CompositingMode; }
            [RemoteExecution]
            set { g.CompositingMode = value; }
        }

        public System.Drawing.Drawing2D.CompositingQuality CompositingQuality
        {
            [RemoteExecution]
            get { return g.CompositingQuality; }
            [RemoteExecution]
            set { g.CompositingQuality = value; }
        }

        public float DpiX
        {
            [RemoteExecution]
            get { return g.DpiX; }
        }

        public float DpiY
        {
            [RemoteExecution]
            get { return g.DpiY; }
        }

        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode
        {
            [RemoteExecution]
            get { return g.InterpolationMode; }
            [RemoteExecution]
            set { g.InterpolationMode = value; }
        }

        public bool IsClipEmpty
        {
            [RemoteExecution]
            get { return g.IsClipEmpty; }
        }

        public bool IsVisibleClipEmpty
        {
            [RemoteExecution]
            get { return g.IsVisibleClipEmpty; }
        }

        public float PageScale
        {
            [RemoteExecution]
            get { return g.PageScale; }
            [RemoteExecution]
            set { g.PageScale = value; }
        }

        public GraphicsUnit PageUnit
        {
            [RemoteExecution]
            get { return g.PageUnit; }
            [RemoteExecution]
            set { g.PageUnit = value; }
        }

        public System.Drawing.Drawing2D.PixelOffsetMode PixelOffsetMode
        {
            [RemoteExecution]
            get { return g.PixelOffsetMode; }
            [RemoteExecution]
            set { g.PixelOffsetMode = value; }
        }

        public Point RenderingOrigin
        {
            [RemoteExecution]
            get { return g.RenderingOrigin; }
            [RemoteExecution]
            set { g.RenderingOrigin = value; }
        }

        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            [RemoteExecution]
            get { return g.SmoothingMode; }
            [RemoteExecution]
            set { g.SmoothingMode = value; }
        }

        public int TextContrast
        {
            [RemoteExecution]
            get { return g.TextContrast; }
            [RemoteExecution]
            set { g.TextContrast = value; }
        }

        public System.Drawing.Text.TextRenderingHint TextRenderingHint
        {
            [RemoteExecution]
            get { return g.TextRenderingHint; }
            [RemoteExecution]
            set { g.TextRenderingHint = value; }
        }

        public System.Drawing.Drawing2D.Matrix Transform
        {
            [RemoteExecution]
            get { return g.Transform; }
            [RemoteExecution]
            set { g.Transform = value; }
        }

        public RectangleF VisibleClipBounds
        {
            [RemoteExecution]
            get { return g.VisibleClipBounds; }
        }

        [RemoteExecution]
        public void Clear(Color color)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void Dispose()
        {
            g.Dispose();
        }

        [RemoteExecution]
        public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            g.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            g.DrawArc(pen, rect, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            g.DrawArc(pen, x, y, width, height, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
        {
            g.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        [RemoteExecution]
        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
        {
            g.DrawBezier(pen, pt1, pt2, pt3, pt4);
        }

        [RemoteExecution]
        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
        {
            g.DrawBezier(pen, x1, y1, x2, y2, x3, y3, x4, y4);
        }

        [RemoteExecution]
        public void DrawBeziers(Pen pen, Point[] points)
        {
            g.DrawBeziers(pen, points);
        }

        [RemoteExecution]
        public void DrawBeziers(Pen pen, PointF[] points)
        {
            g.DrawBeziers(pen, points);
        }

        [RemoteExecution]
        public void DrawClosedCurve(Pen pen, Point[] points)
        {
            g.DrawClosedCurve(pen, points);
        }

        [RemoteExecution]
        public void DrawClosedCurve(Pen pen, PointF[] points)
        {
            g.DrawClosedCurve(pen, points);
        }

        [RemoteExecution]
        public void DrawClosedCurve(Pen pen, Point[] points, float tension, System.Drawing.Drawing2D.FillMode fillmode)
        {
            g.DrawClosedCurve(pen, points, tension, fillmode);
        }

        [RemoteExecution]
        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, System.Drawing.Drawing2D.FillMode fillmode)
        {
            g.DrawClosedCurve(pen, points, tension, fillmode);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, Point[] points)
        {
            g.DrawCurve(pen, points);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, PointF[] points)
        {
            g.DrawCurve(pen, points);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, Point[] points, float tension)
        {
            g.DrawCurve(pen, points, tension);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, PointF[] points, float tension)
        {
            g.DrawCurve(pen, points, tension);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
        {
            g.DrawCurve(pen, points, offset, numberOfSegments);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
        {
            g.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        [RemoteExecution]
        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
        {
            g.DrawCurve(pen, points, offset, numberOfSegments, tension);
        }

        [RemoteExecution]
        public void DrawEllipse(Pen pen, Rectangle rect)
        {
            g.DrawEllipse(pen, rect);
        }

        [RemoteExecution]
        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            g.DrawEllipse(pen, rect);
        }

        [RemoteExecution]
        public void DrawEllipse(Pen pen, float x, float y, float width, float height)
        {
            g.DrawEllipse(pen, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawEllipse(Pen pen, int x, int y, int width, int height)
        {
            g.DrawEllipse(pen, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawIcon(Icon icon, Rectangle targetRect)
        {
            g.DrawIcon(icon, targetRect);
        }

        [RemoteExecution]
        public void DrawIcon(Icon icon, int x, int y)
        {
            g.DrawIcon(icon, x, y);
        }

        [RemoteExecution]
        public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
        {
            g.DrawIconUnstretched(icon, targetRect);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Point point)
        {
            g.DrawImage(image, point);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Point[] destPoints)
        {
            g.DrawImage(image, destPoints);
        }

        [RemoteExecution]
        public void DrawImage(Image image, PointF point)
        {
            g.DrawImage(image, point);
        }

        [RemoteExecution]
        public void DrawImage(Image image, PointF[] destPoints)
        {
            g.DrawImage(image, destPoints);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle rect)
        {
            g.DrawImage(image, rect);
        }

        [RemoteExecution]
        public void DrawImage(Image image, RectangleF rect)
        {
            g.DrawImage(image, rect);
        }

        [RemoteExecution]
        public void DrawImage(Image image, float x, float y)
        {
            g.DrawImage(image, x, y);
        }

        [RemoteExecution]
        public void DrawImage(Image image, int x, int y)
        {
            g.DrawImage(image, x, y);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, destPoints, srcRect, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, destRect, srcRect, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, destRect, srcRect, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, float x, float y, float width, float height)
        {
            g.DrawImage(image, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, x, y, srcRect, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, int x, int y, int width, int height)
        {
            g.DrawImage(image, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, x, y, srcRect, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
        }

        [RemoteExecution]
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            g.DrawImage(image, destPoints, srcRect, srcUnit, imageAttr);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
        {
            g.DrawImage(image, destRect, srcX, srcY, srcWidth, srcHeight, srcUnit);
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttrs)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttrs, Graphics.DrawImageAbort callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, System.Drawing.Imaging.ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawImageUnscaled(Image image, Point point)
        {
            g.DrawImageUnscaled(image, point);
        }

        [RemoteExecution]
        public void DrawImageUnscaled(Image image, Rectangle rect)
        {
            g.DrawImageUnscaled(image, rect);
        }

        [RemoteExecution]
        public void DrawImageUnscaled(Image image, int x, int y)
        {
            g.DrawImageUnscaled(image, x, y);
        }

        [RemoteExecution]
        public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
        {
            g.DrawImageUnscaled(image, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
        {
            g.DrawImageUnscaledAndClipped(image, rect);
        }

        [RemoteExecution]
        public void DrawLine(Pen pen, Point pt1, Point pt2)
        {
            g.DrawLine(pen, pt1, pt2);
        }

        [RemoteExecution]
        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            g.DrawLine(pen, pt1, pt2);
        }

        [RemoteExecution]
        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        [RemoteExecution]
        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
        {
            g.DrawLine(pen, x1, y1, x2, y2);
        }

        [RemoteExecution]
        public void DrawLines(Pen pen, Point[] points)
        {
            g.DrawLines(pen, points);
        }

        [RemoteExecution]
        public void DrawLines(Pen pen, PointF[] points)
        {
            g.DrawLines(pen, points);
        }

        [RemoteExecution]
        public void DrawPath(Pen pen, System.Drawing.Drawing2D.GraphicsPath path)
        {
            g.DrawPath(pen, path);
        }

        [RemoteExecution]
        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
        {
            g.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
        {
            g.DrawPie(pen, rect, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            g.DrawPie(pen, x, y, width, height, startAngle, sweepAngle);
        }

        [RemoteExecution]
        public void DrawPolygon(Pen pen, Point[] points)
        {
            g.DrawPolygon(pen, points);
        }

        [RemoteExecution]
        public void DrawPolygon(Pen pen, PointF[] points)
        {
            g.DrawPolygon(pen, points);
        }

        [RemoteExecution]
        public void DrawRectangle(Pen pen, Rectangle rect)
        {
            g.DrawRectangle(pen, rect);
        }

        [RemoteExecution]
        public void DrawRectangle(Pen pen, float x, float y, float width, float height)
        {
            g.DrawRectangle(pen, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawRectangle(Pen pen, int x, int y, int width, int height)
        {
            g.DrawRectangle(pen, x, y, width, height);
        }

        [RemoteExecution]
        public void DrawRectangles(Pen pen, Rectangle[] rects)
        {
            g.DrawRectangles(pen, rects);
        }

        [RemoteExecution]
        public void DrawRectangles(Pen pen, RectangleF[] rects)
        {
            g.DrawRectangles(pen, rects);
        }

        [RemoteExecution]
        public void DrawString(string s, Font font, Brush brush, PointF point)
        {
            g.DrawString(s, font, brush, point);
        }

        [RemoteExecution]
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            g.DrawString(s, font, brush, layoutRectangle);
        }

        [RemoteExecution]
        public void DrawString(string s, Font font, Brush brush, float x, float y)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EndContainer(System.Drawing.Drawing2D.GraphicsContainer container)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void EnumerateMetafile(System.Drawing.Imaging.Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, System.Drawing.Imaging.ImageAttributes imageAttr)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void ExcludeClip(Rectangle rect)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void ExcludeClip(Region region)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillClosedCurve(Brush brush, Point[] points)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillClosedCurve(Brush brush, PointF[] points)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillClosedCurve(Brush brush, Point[] points, System.Drawing.Drawing2D.FillMode fillmode)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillClosedCurve(Brush brush, PointF[] points, System.Drawing.Drawing2D.FillMode fillmode)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillClosedCurve(Brush brush, Point[] points, System.Drawing.Drawing2D.FillMode fillmode, float tension)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillClosedCurve(Brush brush, PointF[] points, System.Drawing.Drawing2D.FillMode fillmode, float tension)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillEllipse(Brush brush, Rectangle rect)
        {
            g.FillEllipse(brush, rect);
        }

        [RemoteExecution]
        public void FillEllipse(Brush brush, RectangleF rect)
        {
            g.FillEllipse(brush, rect);
        }

        [RemoteExecution]
        public void FillEllipse(Brush brush, float x, float y, float width, float height)
        {
            g.FillEllipse(brush, x, y, width, height);
        }

        [RemoteExecution]
        public void FillEllipse(Brush brush, int x, int y, int width, int height)
        {
            g.FillEllipse(brush, x, y, width, height);
        }

        [RemoteExecution]
        public void FillPath(Brush brush, System.Drawing.Drawing2D.GraphicsPath path)
        {
            g.FillPath(brush, path);
        }

        [RemoteExecution]
        public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillPolygon(Brush brush, Point[] points)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillPolygon(Brush brush, PointF[] points)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillPolygon(Brush brush, Point[] points, System.Drawing.Drawing2D.FillMode fillMode)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillPolygon(Brush brush, PointF[] points, System.Drawing.Drawing2D.FillMode fillMode)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillRectangle(Brush brush, Rectangle rect)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillRectangle(Brush brush, RectangleF rect)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillRectangle(Brush brush, float x, float y, float width, float height)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void FillRectangle(Brush brush, int x, int y, int width, int height)
        {
            g.FillRectangle(brush, x, y, width, height);
        }

        [RemoteExecution]
        public void FillRectangles(Brush brush, Rectangle[] rects)
        {
            g.FillRectangles(brush, rects);
        }

        [RemoteExecution]
        public void FillRectangles(Brush brush, RectangleF[] rects)
        {
            g.FillRectangles(brush, rects);
        }

        [RemoteExecution]
        public void FillRegion(Brush brush, Region region)
        {
            g.FillRegion(brush, region);
        }

        [RemoteExecution]
        public void Flush()
        {
            g.Flush();
        }

        [RemoteExecution]
        public void Flush(System.Drawing.Drawing2D.FlushIntention intention)
        {
            g.Flush(intention);
        }

        [RemoteExecution]
        public object GetContextInfo()
        {
            return g.GetContextInfo();
        }

        [RemoteExecution]
        public IntPtr GetHdc()
        {
            return g.GetHdc();
        }

        [RemoteExecution]
        public Color GetNearestColor(Color color)
        {
            return g.GetNearestColor(color);
        }

        [RemoteExecution]
        public void IntersectClip(Rectangle rect)
        {
            g.IntersectClip(rect);
        }

        [RemoteExecution]
        public void IntersectClip(RectangleF rect)
        {
            g.IntersectClip(rect);
        }

        [RemoteExecution]
        public void IntersectClip(Region region)
        {
            g.IntersectClip(region);
        }

        [RemoteExecution]
        public bool IsVisible(Point point)
        {
            return g.IsVisible(point);
        }

        [RemoteExecution]
        public bool IsVisible(PointF point)
        {
            return g.IsVisible(point);
        }

        [RemoteExecution]
        public bool IsVisible(Rectangle rect)
        {
            return g.IsVisible(rect);
        }

        [RemoteExecution]
        public bool IsVisible(RectangleF rect)
        {
            return g.IsVisible(rect);
        }

        [RemoteExecution]
        public bool IsVisible(float x, float y)
        {
            return g.IsVisible(x, y);
        }

        [RemoteExecution]
        public bool IsVisible(int x, int y)
        {
            return g.IsVisible(x, y);
        }

        [RemoteExecution]
        public bool IsVisible(float x, float y, float width, float height)
        {
            return g.IsVisible(x, y, width, height);
        }

        [RemoteExecution]
        public bool IsVisible(int x, int y, int width, int height)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
        {
            return g.MeasureCharacterRanges(text, font, layoutRect, stringFormat);
        }

        [RemoteExecution]
        public SizeF MeasureString(string text, Font font)
        {
            return g.MeasureString(text, font);
        }

        [RemoteExecution]
        public SizeF MeasureString(string text, Font font, int width)
        {
            return g.MeasureString(text, font, width);
        }

        [RemoteExecution]
        public SizeF MeasureString(string text, Font font, SizeF layoutArea)
        {
            return g.MeasureString(text, font, layoutArea);
        }

        [RemoteExecution]
        public SizeF MeasureString(string text, Font font, int width, StringFormat format)
        {
            return g.MeasureString(text, font, width, format);
        }

        [RemoteExecution]
        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
        {
            return g.MeasureString(text, font, origin, stringFormat);
        }

        [RemoteExecution]
        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
        {
            return g.MeasureString(text, font, layoutArea, stringFormat);
        }

        [RemoteExecution]
        public void MultiplyTransform(System.Drawing.Drawing2D.Matrix matrix)
        {
            g.MultiplyTransform(matrix);
        }

        [RemoteExecution]
        public void MultiplyTransform(System.Drawing.Drawing2D.Matrix matrix, System.Drawing.Drawing2D.MatrixOrder order)
        {
            g.MultiplyTransform(matrix, order);
        }

        [RemoteExecution]
        public void ReleaseHdc()
        {
            g.ReleaseHdc();
        }

        [RemoteExecution]
        public void ReleaseHdc(IntPtr hdc)
        {
            g.ReleaseHdc(hdc);
        }

        [RemoteExecution]
        public void ReleaseHdcInternal(IntPtr hdc)
        {
            g.ReleaseHdcInternal(hdc);
        }

        [RemoteExecution]
        public void ResetClip()
        {
            g.ResetClip();
        }

        [RemoteExecution]
        public void ResetTransform()
        {
            g.ResetTransform();
        }

        [RemoteExecution]
        public void Restore(System.Drawing.Drawing2D.GraphicsState gstate)
        {
            g.Restore(gstate);
        }

        [RemoteExecution]
        public void RotateTransform(float angle)
        {
            g.RotateTransform(angle);
        }

        [RemoteExecution]
        public void RotateTransform(float angle, System.Drawing.Drawing2D.MatrixOrder order)
        {
            g.RotateTransform(angle, order);
        }

        [RemoteExecution]
        public System.Drawing.Drawing2D.GraphicsState Save()
        {
            return g.Save();
        }

        [RemoteExecution]
        public void ScaleTransform(float sx, float sy)
        {
            g.ScaleTransform(sx, sy);
        }

        [RemoteExecution]
        public void ScaleTransform(float sx, float sy, System.Drawing.Drawing2D.MatrixOrder order)
        {
            g.ScaleTransform(sx, sy, order);
        }

        [RemoteExecution]
        public void SetClip(Graphics g)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void SetClip(System.Drawing.Drawing2D.GraphicsPath path)
        {
            g.SetClip(path);
        }

        [RemoteExecution]
        public void SetClip(Rectangle rect)
        {
            g.SetClip(rect);
        }

        [RemoteExecution]
        public void SetClip(RectangleF rect)
        {
            g.SetClip(rect);
        }

        [RemoteExecution]
        public void SetClip(Graphics g, System.Drawing.Drawing2D.CombineMode combineMode)
        {
            throw new NotImplementedException();
        }

        [RemoteExecution]
        public void SetClip(System.Drawing.Drawing2D.GraphicsPath path, System.Drawing.Drawing2D.CombineMode combineMode)
        {
            g.SetClip(path, combineMode);
        }

        [RemoteExecution]
        public void SetClip(Rectangle rect, System.Drawing.Drawing2D.CombineMode combineMode)
        {
            g.SetClip(rect, combineMode);
        }

        [RemoteExecution]
        public void SetClip(RectangleF rect, System.Drawing.Drawing2D.CombineMode combineMode)
        {
            g.SetClip(rect, combineMode);
        }

        [RemoteExecution]
        public void SetClip(Region region, System.Drawing.Drawing2D.CombineMode combineMode)
        {
            g.SetClip(region, combineMode);
        }

        [RemoteExecution]
        public void TransformPoints(System.Drawing.Drawing2D.CoordinateSpace destSpace, System.Drawing.Drawing2D.CoordinateSpace srcSpace, Point[] pts)
        {
            g.TransformPoints(destSpace, srcSpace, pts);
        }

        [RemoteExecution]
        public void TransformPoints(System.Drawing.Drawing2D.CoordinateSpace destSpace, System.Drawing.Drawing2D.CoordinateSpace srcSpace, PointF[] pts)
        {
            g.TransformPoints(destSpace, srcSpace, pts);
        }

        [RemoteExecution]
        public void TranslateClip(float dx, float dy)
        {
            g.TranslateClip(dx, dy);
        }

        [RemoteExecution]
        public void TranslateClip(int dx, int dy)
        {
            g.TranslateClip(dx, dy);
        }

        [RemoteExecution]
        public void TranslateTransform(float dx, float dy)
        {
            g.TranslateTransform(dx, dy);
        }

        [RemoteExecution]
        public void TranslateTransform(float dx, float dy, System.Drawing.Drawing2D.MatrixOrder order)
        {
            g.TranslateTransform(dx, dy, order);
        }
    }
}