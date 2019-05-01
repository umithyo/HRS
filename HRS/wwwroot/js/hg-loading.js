/**
 * Author and copyright: Helder Gonzaga
 * Repository: https://github.com/helder.gonzaga/loading
 * License: MIT, see file 'LICENSE'
 */

(function ($) {
    "use strict"

    var i = 0
    function Loading(props) {
        this.props = {
            body: "", // the dialog body html
            loadingClass: "fade", // Additional css for ".Loading", "fade" for fade effect
            loadingDialogClass: "", // Additional css for ".Loading-dialog", like "Loading-lg" or "Loading-sm" for sizing
            options: null, 
            target: "",
            imgLoading: "assets/img/loading.svg", 
            // Events:
            onCreate: null, // Callback, called after the Loading was created
            onDispose: null, // Callback, called after the Loading was disposed
            onSubmit: null // Callback of $.showConfirm(), called after yes or no was pressed
        }
        for (var prop in props) {
            this.props[prop] = props[prop]
        }
        this.id = "hg-show-loading-" + i
        i++
        this.show()
    }

    Loading.prototype.createContainerElement = function () {
        var self = this
        
        this.element = document.createElement("div")
        this.element.id = this.id
        

        if(this.props.target != ""){
            this.element.setAttribute("class", "hg-loading-int ")
        }else{
            this.element.setAttribute("class", "hg-loading ")
        }
        this.element.setAttribute("tabindex", "-1")
        this.element.setAttribute("role", "progressbar")
        this.element.setAttribute("aria-labelledby", this.id) 
        this.element.innerHTML = '<div class="loading-dialog ' + this.props.loadingDialogClass + '" role="document" >' +
            '<div class="loading-body"> <img src="' + this.props.imgLoading + '" alt="Loading ..."></div>' +
            '</div>';

        if(this.props.target != ""){
        	document.querySelector('#'+this.props.target).appendChild(this.element);
        }else{
        	document.body.appendChild(this.element)
        }


        this.bodyElement = this.element.querySelector(".loading-body")
        
        if (this.props.onCreate) {
            this.props.onCreate(this)
        }
        $.id_loading = this.id;
        $.loadingClass = this.props.loadingClass;
        $.hg_target = this.props.target;
        

        setTimeout(function () {
            if($.hg_target != ""){
                document.querySelector('#'+$.id_loading).setAttribute("class", "hg-loading-int " + $.loadingClass)
            }else{
                document.querySelector('#'+$.id_loading).setAttribute("class", "hg-loading " + $.loadingClass)
            }
           
        }, 500);


    }

    Loading.prototype.show = function () {
        if (!this.element) {
            this.createContainerElement();
        } else {
            $(this.element).loading('show')
        }
        
    }

    $.extend({
        showLoading: function (props) {
            return new Loading(props)
        },
        hideLoading: function () {
            var element = document.querySelector('#' + $.id_loading)
            element.setAttribute("class", "hg-loading ")
            setTimeout(function () {
                if (element)
                document.body.removeChild(element);
                $.id_loading = null;
                $.loadingClass = null;
            }, 500);
        }
    })

}(jQuery))