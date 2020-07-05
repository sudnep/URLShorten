<template>
  <q-page class="flex">
    <q-card class="my-card" style="width:100%;">
      <div class="text-h6">Short URL Generator</div>
      <q-form @submit="onSubmit" @reset="onReset" class="q-gutter-md">
        <q-input
          filled
          debounce="500"
          v-model="originalURL"
          label="Long URL or Link"
          :rules="[checkExisting]"
          @input="checkExisting"
        />

        <q-input
          filled
          type="text"
          v-model="slug"
          label="Slug. Leave blank to
        autogenerate or specifiy your unique slug"
          hint="Slug or Unique keyword
        for the link"
          debounce="500"
          @input="checkSlug"
          :rules="[checkSlug]"
        />

        <div>
          <q-btn class="q-ml-md" label="Submit" type="submit" color="dark" />
          <q-btn
            label="Reset"
            type="reset"
            color="primary"
            flat
            class="q-ml-sm"
          />
        </div>
        <div v-if="shortUrl">
          <q-banner dense class="bg-grey-3">
            Short version of the link has been created and
            <q-chip icon="check" color="teal" text-color="white"
              >Copied!</q-chip
            >
            to clipboard.

            <a
              ref="link"
              target="_blank"
              style=""
              class="q-pa-lg text-green"
              :href="shortUrl"
              >{{ shortUrl }}</a
            >

            <template v-slot:action>
              <q-btn
                @click="copyLink"
                flat
                round
                icon="mdi-content-copy"
              ></q-btn>
            </template>
          </q-banner>
        </div>
      </q-form>
    </q-card>
  </q-page>
</template>

<script>
import { Notify } from "quasar";
export default {
  data() {
    return {
      originalURL: "",
      slug: "",
      baseURL: "http://sp.mufg",
      isSlugValid: "",
      accept: false,
      tooltip: false,
      shortUrl: "",
      error: false,
      errorMessage: ""
    };
  },

  methods: {
    onSubmit() {
      var that = this;
      const options = {
        method: "post",
        url: "http://localhost:5000/api/URL/Create",
        data: {
          originalURl: this.originalURL,
          baseUrl: this.baseURL,
          urlCode: this.slug
        }
      };
      this.$axios(options)
        .then(function(response) {
          // handle success
          console.log(response);
          that.shortUrl = response.data.shortUrl;
        })
        .catch(function(error) {
          // handle error
          console.log(error);
        })
        .then(function() {
          that.copyLink();
        });

      this.$q.notify({
        color: "green-4",
        position: "top",
        textColor: "white",
        icon: "mdi-content-copy",
        message: "Short linked created and copied !"
      });
    },

    onReset() {
      this.originalURL = null;
      this.slug = null;
      this.shortUrl = "";
    },
    copyLink() {
      var link = this.$refs.link;
      const range = document.createRange();
      range.selectNode(link);
      const selection = window.getSelection();
      selection.removeAllRanges();
      selection.addRange(range);
      const successful = document.execCommand("copy");
      document.execCommand("copy");
      selection.removeAllRanges();
      this.tooltip = true;
      setTimeout((this.tooltip = false), 2000);
    },
    checkSlug() {
      if (this.slug === "") {
        this.error = true;
        this.errorMessage = "Slug is empty";
        return;
      }

      var that = this;
      const options = {
        method: "get",
        url: "http://localhost:5000/api/URL/" + this.slug + "/check"
      };

      return new Promise(function(resolve, reject) {
        if (!/^[a-zA-Z0-9]{0,20}$/.test(that.slug)) {
          resolve(false || "No special char allowed in slug");
        } else if (that.slug.length > 20) {
          resolve(false || "Slug length should be less than 20 characters");
        }
        that
          .$axios(options)
          .then(function(response) {
            // handle success
            console.log(response);
            resolve(
              (response.data && /^[a-zA-Z0-9]{0,20}$/.test(that.slug)) ||
                "Slug is already in use, try another."
            );
          })
          .catch(function(error) {
            // handle error
            console.log(error);
          })
          .then(function() {
            // always executed
          });
      });
    },
    checkExisting() {
      var that = this;
      const options = {
        method: "post",
        url: "http://localhost:5000/api/URL/checkfull",
        data: {
          originalURl: that.originalURL
        }
      };

      return new Promise(function(resolve, reject) {
        if (that.originalURL.length < 1) {
          resolve(false || "Please provide url to shorten.");
        }
        that
          .$axios(options)
          .then(function(response) {
            // handle success
            console.log(response);
            resolve(
              !response.data ||
                "Short url already exists for this. See short url below"
            );
            if (response.data?.originalURl) {
              that.shortUrl = response.data.shortUrl;
              that.copyLink();
            }
          })
          .catch(function(error) {
            // handle error
            console.log(error);
          })
          .then(function() {
            // always executed
          });
      });
    }
  }
};
</script>
