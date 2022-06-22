class Point{
    x:number = 0;
    y:number = 0;
    constructor(x:number = 0, y:number = 0){
        this.x = x;
        this.y = y;
    }
}

class UiMouseEvent{
    globalLocaton:Point = new Point();
    location:Point = new Point();
    target:DisplayObject;
}

class DisplayObject{
    x:number = 0;
    y:number = 0;
    width:number = 0;
    height:number = 0;
    backgroundColor:string = "";
    borderColor:string = "";
    borderThickness:number = 0;
    isVisible:boolean = true;

    constructor(x:number = 0,y:number = 0,width:number = 100,height:number = 100){
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }
    
    parent:DisplayObject;

    getStage():Stage{
        if(this instanceof Stage) return this as Stage;
        else if(this.parent != null) return this.parent.getStage();
        else return null;
    }

    capture(){
        let stage = this.getStage();
        if(stage != null) stage.captureObj = this;
        console.log("capture");
    }

    releaseCapture(){
        let stage = this.getStage();
        if(stage != null){
            console.log("releaseCapture");
            if(stage.captureObj == this) stage.captureObj = null;
        }
    }

    onclick:Function = null;
    onmousedown:Function = null;
    onmouseup:Function = null;
    onmouseover:Function = null;
    onmouseenter:Function = null;
    onmouseleave:Function = null;
    onmousemove:Function = null;

    hitTestEnable:boolean = true;

    measure(){
    }

    localToGlobal(p:Point):Point{
        if(p == null) return null;
        if(this.parent == null) return new Point(p.x,p.y);
        else return this.parent.localToGlobal(new Point(p.x + this.x, p.y + this.y));
    }

    cvtMouseEvent(e:MouseEvent):UiMouseEvent {
        if(e == null) return null;
        let p:Point = new Point(e.offsetX,e.offsetY);
        let p2:Point = this.globalToLocal(p);
        let ue:UiMouseEvent = new UiMouseEvent();
        ue.globalLocaton = p;
        ue.location = p2;
        ue.target = this;
        return ue;
    }

    globalToLocal(p:Point):Point{
        if(p == null) return null;
        if(this.parent == null) return new Point(p.x,p.y);
        else return this.parent.globalToLocal(new Point(p.x - this.x, p.y - this.y));
    }

    hitTest(x:number, y:number){
        if(this.hitTestEnable == false || this.isVisible == false) return false;
        if(x < 0 || x > this.width || y < 0 || y > this.height) return false;
        else return true;
    }

    hitTestObj(x:number, y:number):DisplayObject{
        if(this.hitTest(x,y)) return this;
        else return null;
    }

    draw(ctx:CanvasRenderingContext2D){
        if(this.isVisible == false) return;
        if(this.backgroundColor !== "" && this.width > 0 && this.height > 0){
            ctx.fillStyle = this.backgroundColor;
            ctx.fillRect(0,0,this.width, this.height);
        }
        if(this.borderColor != "" && this.borderThickness > 0){
            ctx.strokeStyle = this.borderColor;
            ctx.lineWidth = this.borderThickness;
            ctx.strokeRect(0,0,this.width,this.height);
        }
    }
}

class Control extends DisplayObject{
    childrens:Array<DisplayObject> = new Array<DisplayObject>();

    add(item:DisplayObject){
        if(item != null){
            item.parent = this;
            this.childrens.push(item);
        } 
    }

    clear():void{
        this.childrens = new Array<DisplayObject>();
    }

    hitTestObj(x:number, y:number):DisplayObject{
        if(this.hitTestEnable == false) return null;

        for(let i = 0; i < this.childrens.length; i++){
            let item:DisplayObject = this.childrens[i];
            let xx = x - item.x;
            let yy = y - item.y;
            let match = item.hitTestObj(xx,yy);
            if(match != null) return match;
        }
        return super.hitTestObj(x,y);
    }

    draw(ctx:CanvasRenderingContext2D){
        if(this.isVisible == false) return;
        super.draw(ctx);
        this.childrens.forEach(element => {
            element.measure();
            if(element.x < this.width && element.x + element.width > 0 && element.y < this.height && element.y + element.height > 0){
                ctx.translate(element.x,element.y);
                element.draw(ctx);            
                ctx.translate(-element.x,-element.y);    
            }
        });
    }
}

class Stage extends Control{
    canvas:HTMLCanvasElement;
    width :number = 0;
    height:number = 0;
    hoverElement:DisplayObject = null;
    captureObj:DisplayObject = null;

    constructor(id:string){
        super();
        let self = this;
        this.canvas = document.getElementById(id) as HTMLCanvasElement;        
        
        this.canvas.onmousedown = function(e:MouseEvent){
            self.inner_onmousedown(e,self);
        };
        this.canvas.onmouseup = function(e:MouseEvent){
            self.inner_onmouseup(e,self);
        };
        this.canvas.onmouseover = function(e:MouseEvent){
            self.inner_onmouseover(e,self);
        };
        this.canvas.onclick = function(e:MouseEvent){
            self.inner_onclick(e,self);
        };
        this.canvas.onmouseenter = function(e:MouseEvent){
            self.inner_onmouseenter(e,self);
        };
        this.canvas.onmouseleave = function(e:MouseEvent){
            self.inner_onmouseleave(e,self);
        };
        this.canvas.onmousemove = function(e:MouseEvent){
            self.inner_onmousemove(e,self);
        };

        this.backgroundColor = "#CCCCCC";
        self.resize();
    }

    capture(){
        this.captureObj = this;
    }

    releaseCapture(){
        this.captureObj = null;
    }

    pick(e:MouseEvent, preferCapture:boolean = false):DisplayObject{
        if(preferCapture == true && this.captureObj != null) return this.captureObj;
        let match:DisplayObject = this.hitTestObj(e.offsetX,e.offsetY);
        if(match == null) match = this;
        return match;
    }

    inner_onmouseover(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e);
        if(match.onmouseover != null) match.onmouseover(match.cvtMouseEvent(e));
    }

    inner_onmousedown(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e);
        if(match.onmousedown != null) match.onmousedown(match.cvtMouseEvent(e));
    }

    inner_onmouseup(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e, true);
        if(match.onmouseup != null) match.onmouseup(match.cvtMouseEvent(e));
    }

    inner_onclick(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e);
        if(match.onclick != null) match.onclick(match.cvtMouseEvent(e));
    }

    inner_onmouseenter(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e);
        if(match.onmouseenter != null) match.onmouseenter(match.cvtMouseEvent(e));
    }

    inner_onmouseleave(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e,true);
        if(this.hoverElement != null){
            if(this.hoverElement.onmouseleave != null) this.hoverElement.onmouseleave(this.hoverElement.cvtMouseEvent(e));
        }
        if(match.onmouseleave != null) match.onmouseleave(match.cvtMouseEvent(e));
        self.captureObj = null;
    }

    inner_onmousemove(e:MouseEvent, self:Stage){
        let match:DisplayObject = self.pick(e,true);
        if(this.hoverElement != match){
            if(this.hoverElement != null){
                if(this.hoverElement.onmouseleave != null) this.hoverElement.onmouseleave(this.hoverElement.cvtMouseEvent(e));
            }
            this.hoverElement = match;
            if(this.hoverElement != null){
                if(this.hoverElement.onmouseenter != null) this.hoverElement.onmouseenter(this.hoverElement.cvtMouseEvent(e));
            }
        }
        if(match.onmousemove != null) match.onmousemove(match.cvtMouseEvent(e));
    }

    resize():void{
        this.canvas.width = this.canvas.clientWidth;
        this.width = this.canvas.width;
        this.height = this.canvas.height;
    }

    render():void{
        let ctx:CanvasRenderingContext2D = this.canvas.getContext("2d") as CanvasRenderingContext2D;
        if(ctx != null){
            ctx.clearRect(0,0,this.width, this.height);
            this.draw(ctx);
        }
    }
}